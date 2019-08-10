using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gerontocracy.Core.BusinessObjects.Account;
using Gerontocracy.Core.BusinessObjects.Shared;
using Gerontocracy.Core.BusinessObjects.User;
using Gerontocracy.Core.Exceptions.User;
using Gerontocracy.Core.Interfaces;
using Gerontocracy.Data;
using Gerontocracy.Shared.Extensions;
using Npgsql;

using db = Gerontocracy.Data.Entities;

namespace Gerontocracy.Core.Providers
{
    public class UserService : IUserService
    {
        private readonly GerontocracyContext _context;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

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
            "WHERE  users.\"UserName\" LIKE @userName " +
            "GROUP  BY users.\"Id\", " +
            "          users.\"UserName\", " +
            "          users.\"RegisterDate\" " +
            "OFFSET @offset " +
            "LIMIT  @limit ";

        private const string UserDetailQuery =
            "SELECT roles.\"Name\" " +
            "FROM   \"AspNetRoles\" roles " +
            "       JOIN \"AspNetUserRoles\" userRoles " +
            "         ON roles.\"Id\" = userRoles.\"RoleId\" " +
            "WHERE  userRoles.\"UserId\" = @userId ";


        public UserService(
            GerontocracyContext context,
            IAccountService accountService,
            IMapper mapper)
        {
            this._context = context;
            this._accountService = accountService;
            this._mapper = mapper;
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

            var affairCount = _context.Vorfall.Count(n => n.UserId == id);

            var roles = _context.GetData(
                UserDetailQuery,
                reader => reader.GetString(0),
                new NpgsqlParameter<long>("userId", id).AsList().ToArray())
                .ToList();

            return new UserDetail
            {
                Id = dbUser.Id,
                UserName = dbUser.UserName,
                AccessFailedCount = dbUser.AccessFailedCount,
                EmailConfirmed = dbUser.EmailConfirmed,
                LockoutEnd = dbUser.LockoutEnd,
                RegisterDate = dbUser.RegisterDate,
                VorfallCount = affairCount,
                Roles = roles
            };
        }
    }
}
