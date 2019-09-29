﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Gerontocracy.Core.BusinessObjects.Account;
using Gerontocracy.Core.BusinessObjects.Shared;
using Gerontocracy.Core.BusinessObjects.User;
using Gerontocracy.Core.Exceptions.User;
using Gerontocracy.Core.Interfaces;
using Gerontocracy.Data;
using Gerontocracy.Data.Entities.Affair;
using Gerontocracy.Data.Entities.Board;
using Gerontocracy.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

using bo = Gerontocracy.Core.BusinessObjects.User;

namespace Gerontocracy.Core.Providers
{
    public class UserService : IUserService
    {
        private readonly GerontocracyContext _context;
        private readonly IAccountService _accountService;

        private const string UserQuery =
            "SELECT users.\"Id\", " +
            "       users.\"UserName\", " +
            "       users.\"RegisterDate\", " +
            "       coalesce(string_agg(roles.\"Name\", ';'),'') " +
            "FROM   \"AspNetUsers\" users " +
            "       LEFT OUTER JOIN \"AspNetUserRoles\" matching " +
            "                    ON users.\"Id\" = matching.\"UserId\" " +
            "       LEFT OUTER JOIN \"AspNetRoles\" AS roles " +
            "                    ON roles.\"Id\" = matching.\"RoleId\" " +
            "WHERE  users.\"UserName\" ILIKE @userName " +
            "GROUP  BY users.\"Id\", " +
            "          users.\"UserName\", " +
            "          users.\"RegisterDate\" " +
            "OFFSET @offset " +
            "LIMIT  @limit ";

        private const string UserDetailQuery =
            "SELECT roles.\"Id\", roles.\"Name\" " +
            "FROM   \"AspNetRoles\" roles " +
            "       JOIN \"AspNetUserRoles\" userRoles " +
            "         ON roles.\"Id\" = userRoles.\"RoleId\" " +
            "WHERE  userRoles.\"UserId\" = @userId ";

        public UserService(GerontocracyContext context, IAccountService accountService)
        {
            this._context = context;
            this._accountService = accountService;
        }

        public SearchResult<User> Search(SearchParameters parameters, int pageSize = 25, int pageIndex = 0)
        {
            var countQuery = this._context.Users.AsQueryable();

            var dbParams = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter<int>("limit", pageSize),
                new NpgsqlParameter<int>("offset", pageIndex * pageSize),
                new NpgsqlParameter<string>("userName", $"%{parameters.UserName ?? string.Empty}%")
            };

            if (!string.IsNullOrEmpty(parameters.UserName))
            {
                countQuery = countQuery.Where(n => n.UserName.Contains(parameters.UserName, System.StringComparison.CurrentCultureIgnoreCase));
            }

            var count = countQuery.Count();

            var data = this._context.GetData(UserQuery,
                reader => new User
                {
                    Id = reader.GetInt64(0),
                    UserName = reader.GetString(1),
                    RegisterDate = reader.GetDateTime(2),
                    Roles = reader.GetString(3).Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                },
                dbParams.ToArray());

            var result = new SearchResult<User>()
            {
                Data = data,
                MaxResults = count
            };

            return result;
        }

        public UserDetail GetUserDetail(long id)
        {
            var dbUser = _context.Users.SingleOrDefault(n => n.Id == id);

            if (dbUser == null)
                throw new UserNotFoundException();

            var affairCount = _context.Vorfall.Count(n => n.Id == dbUser.Id);

            var banned = _accountService.TryFindBan(dbUser.Id, out var ban);
            var lockoutEnd = banned ? ban.BanEnd : null;

            var roles = _context.GetData(
                UserDetailQuery,
                reader => new Role
                {
                    Id = reader.GetInt64(0),
                    Name = reader.GetString(1)
                },
                new NpgsqlParameter<long>("userId", dbUser.Id).AsList().ToArray())
                .ToList();

            return new UserDetail
            {
                Id = dbUser.Id,
                UserName = dbUser.UserName,
                AccessFailedCount = dbUser.AccessFailedCount,
                EmailConfirmed = dbUser.EmailConfirmed,
                Banned = banned,
                LockoutEnd = lockoutEnd,
                RegisterDate = dbUser.RegisterDate,
                VorfallCount = affairCount,
                Roles = roles
            };
        }

        public UserData GetUserPageData(ClaimsPrincipal user)
            => GetUserPageData(_accountService.GetIdOfUser(user));

        public UserData GetUserPageData(string name)
            => GetUserPageData(n => n.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

        public UserData GetUserPageData(long id)
            => GetUserPageData(n => n.Id == id);

        public UserData GetUserPageData(Func<UserData, bool> condition)
        {
            var user = this._accountService.GetUserQuery()
                .Include(n => n.Threads)
                .ThenInclude(n => n.InitialPost)
                .Include(n => n.Vorfaelle)
                .ThenInclude(n => n.Legitimitaet)
                .Include(n => n.Vorfaelle)
                .ThenInclude(n => n.Politiker)
                .Include(n => n.Posts)
                .ThenInclude(n => n.Likes)
                .Select(n => new UserData()
                {
                    Id = n.Id,
                    RegisterDate = n.RegisterDate,
                    Score = n.Vorfaelle.Sum(m => m.Legitimitaet.Count(o => o.VoteType == VoteType.Up) - m.Legitimitaet.Count(o => o.VoteType == VoteType.Down)) +
                            n.Posts.Sum(m => m.Likes.Count(o => o.LikeType == LikeType.Like) - m.Likes.Count(o => o.LikeType == LikeType.Dislike)),
                    Name = n.UserName,
                    Affairs = n.Vorfaelle.OrderByDescending(m => m.ErstelltAm).Select(m => new bo.Vorfall()
                    {
                        Id = m.Id,
                        Titel = m.Titel,
                        Reputation = m.Legitimitaet.Count(o => o.VoteType == VoteType.Up) - m.Legitimitaet.Count(o => o.VoteType == VoteType.Down),
                        ErstelltAm = m.ErstelltAm,
                        PolitikerId = m.PolitikerId,
                        PolitikerName = m.Politiker.Name
                    }).ToList(),
                    Threads = n.Threads.Where(m => !m.Deleted && !m.Generated).Select(m => new bo.Thread()
                    {
                        Id = m.Id,
                        Titel = m.Title,
                        CreatedOn = m.InitialPost.CreatedOn,
                    }).ToList(),
                    Posts = n.Posts.Select(m => new bo.Post()
                    {
                        Id = m.Id,
                        Content = m.Content,
                        CreatedOn = m.CreatedOn,
                        Likes = m.Likes.Count(o => o.LikeType == LikeType.Like),
                        Dislikes = m.Likes.Count(o => o.LikeType == LikeType.Dislike)
                    }).ToList()
                }).SingleOrDefault(condition);

            if (user == null)
                throw new UserNotFoundException();

            user.Roles = _context.GetData(
                    UserDetailQuery,
                    reader => new Role
                    {
                        Id = reader.GetInt64(0),
                        Name = reader.GetString(1)
                    },
                    new NpgsqlParameter<long>("userId", user.Id).AsList().ToArray())
                .ToList();

            return user;
        }
    }
}
