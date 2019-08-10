using System;
using AutoMapper;

using Gerontocracy.Core.BusinessObjects.Board;
using Gerontocracy.Core.Exceptions.Board;
using Gerontocracy.Core.Interfaces;
using Gerontocracy.Data;
using Gerontocracy.Shared.Extensions;

using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;

using db = Gerontocracy.Data.Entities.Board;
using Gerontocracy.Core.BusinessObjects.Shared;

namespace Gerontocracy.Core.Providers
{
    public class BoardService : IBoardService
    {
        #region Fields

        private readonly IAccountService _accountService;
        private readonly GerontocracyContext _context;
        private readonly IMapper _mapper;

        private const string PostQuery =
            "WITH recursive post_hierarchy(\"Id\", \"ParentId\") AS " +
            "( " +
            "       SELECT p2.\"Id\", " +
            "              p2.\"ParentId\" " +
            "       FROM   \"Post\" p2 " +
            "       WHERE  p2.\"Id\" = @id " +
            "       UNION ALL " +
            "       SELECT     p3.\"Id\", " +
            "                  p3.\"ParentId\" " +
            "       FROM       \"Post\" p3 " +
            "       INNER JOIN post_hierarchy " +
            "       ON         p3.\"ParentId\" = post_hierarchy.\"Id\" ) " +
            "SELECT          po.\"Id\", " +
            "                po.\"Content\", " +
            "                po.\"CreatedOn\", " +
            "                po.\"ParentId\", " +
            "                po.\"UserId\", " +
            "                u.\"UserName\", " +
            "                ( " +
            "                       SELECT count(*) " +
            "                       FROM   \"Like\" " +
            "                       WHERE  \"LikeType\" = 0 " +
            "                       AND    \"PostId\" = po.\"Id\") AS \"Likes\", " +
            "                ( " +
            "                       SELECT count(*) " +
            "                       FROM   \"Like\" " +
            "                       WHERE  \"LikeType\" = 1 " +
            "                       AND    \"PostId\" = po.\"Id\") AS \"Dislikes\", " +
            "                li.\"LikeType\" " +
            "FROM            \"Post\" po " +
            "JOIN            post_hierarchy hi " +
            "ON              po.\"Id\" = hi.\"Id\" " +
            "JOIN            \"AspNetUsers\" u " +
            "ON              u.\"Id\" = po.\"UserId\" " +
            "LEFT OUTER JOIN \"Like\" li " +
            "ON              li.\"PostId\" = po.\"Id\" " +
            "AND             li.\"UserId\" = @userId";

        private const string ThreadQuery =
            "SELECT          threads.\"Id\", " +
            "                threads.\"UserId\", " +
            "                COALESCE(threads.\"VorfallId\",0) AS vorfallid, " +
            "                posts.\"CreatedOn\", " +
            "                threads.\"Generated\", " +
            "                COALESCE(vorfalls.\"PolitikerId\",0) AS politikerid, " +
            "                threads.\"Title\", " +
            "                users.\"UserName\", " +
            "                COALESCE(politikers.\"AkadGradPre\",'')  AS akadgradpre, " +
            "                COALESCE(politikers.\"Vorname\",'')      AS vorname, " +
            "                COALESCE(politikers.\"Nachname\",'')     AS nachname, " +
            "                COALESCE(politikers.\"AkadGradPost\",'') AS akadgradpost, " +
            "                COALESCE(vorfalls.\"Titel\",'')          AS titel, " +
            "                num.\"NumPosts\" " +
            "FROM            \"Thread\" threads " +
            "JOIN            \"AspNetUsers\" users " +
            "ON              threads.\"UserId\" = users.\"Id\" " +
            "JOIN            \"Post\" posts " +
            "ON              threads.\"InitialPostId\" = posts.\"Id\" " +
            "LEFT OUTER JOIN \"Vorfall\" vorfalls " +
            "ON              threads.\"VorfallId\" = vorfalls.\"Id\" " +
            "LEFT OUTER JOIN \"Politiker\" politikers " +
            "ON              politikers.\"Id\" = vorfalls.\"PolitikerId\" " +
            "JOIN            ( WITH recursive post_hierarchy(\"Id\", \"ParentId\", \"ThreadId\") AS " +
            "                ( " +
            "                       SELECT p2.\"Id\", " +
            "                              p2.\"ParentId\", " +
            "                              th.\"Id\" AS \"ThreadId\" " +
            "                       FROM   \"Post\" p2 " +
            "                       JOIN   \"Thread\" th " +
            "                       ON     th.\"InitialPostId\" = p2.\"Id\" " +
            "                       WHERE  p2.\"ParentId\" IS NULL " +
            "                       UNION ALL " +
            "                       SELECT     p3.\"Id\", " +
            "                                  p3.\"ParentId\", " +
            "                                  post_hierarchy.\"ThreadId\" " +
            "                       FROM       \"Post\" p3 " +
            "                       INNER JOIN post_hierarchy " +
            "                       ON         p3.\"ParentId\" = post_hierarchy.\"Id\" )" +
            "     SELECT   hi.\"ThreadId\", " +
            "              Count(po.\"Id\") AS \"NumPosts\" " +
            "     FROM     \"Post\" po " +
            "     JOIN     post_hierarchy hi " +
            "     ON       po.\"Id\" = hi.\"Id\" " +
            "     JOIN     \"AspNetUsers\" u " +
            "     ON       u.\"Id\" = po.\"UserId\" " +
            "     GROUP BY hi.\"ThreadId\") num " +
            "       ON       num.\"ThreadId\" = threads.\"Id\" " +
            "WHERE    threads.\"Title\" LIKE @title " +
            "LIMIT    @limit " +
            "OFFSET   @offset ";

        #endregion Fields

        #region Constructors

        public BoardService(IMapper mapper, GerontocracyContext context, IAccountService accountService)
        {
            _mapper = mapper;
            _context = context;
            _accountService = accountService;
        }

        #endregion Constructors

        #region Methods

        public long AddThread(ClaimsPrincipal user, ThreadData data)
        {
            var userId = _accountService.GetIdOfUser(user);

            var dbObj = new db.Thread
            {
                Generated = false,
                UserId = userId,
                Title = data.Titel,
                VorfallId = data.VorfallId,
                InitialPost = new db.Post()
                {
                    UserId = userId,
                    Content = data.Content,
                    Likes = new db.Like
                    {
                        LikeType = db.LikeType.Like,
                        UserId = userId
                    }.AsList(),
                }
            };

            _context.Add(dbObj);
            _context.SaveChanges();

            return dbObj.Id;
        }

        public int CountPosts(db.Post post)
            => post.Children.Sum(CountPosts) + 1;

        public ThreadDetail GetThread(ClaimsPrincipal user, long id)
        {
            var userId = _accountService.GetIdOfUserOrDefault(user);

            var thread = _context.Thread
                .Include(n => n.Vorfall).ThenInclude(n => n.Politiker)
                .SingleOrDefault(n => n.Id == id) ??
                         throw new ThreadNotFoundException();

            var dbParams = new NpgsqlParameter[]
            {
                new NpgsqlParameter<long>("id", thread.InitialPostId),
                new NpgsqlParameter<long>("userId", userId.GetValueOrDefault())
            };

            Post ConverterFunction(DbDataReader n) =>
                new Post
                {
                    Id = n.GetInt64(0),
                    Content = n.GetString(1),
                    CreatedOn = n.GetDateTime(2),
                    ParentId = !n.IsDBNull(3) ? n.GetInt64(3) : (long?)null,
                    UserId = n.GetInt64(4),
                    UserName = n.GetString(5),
                    Likes = n.GetInt32(6),
                    Dislikes = n.GetInt32(7),
                    UserLike = !n.IsDBNull(8) ? (LikeType)n.GetInt32(8) : (LikeType?)null
                };

            var posts = this._context.GetData(PostQuery, ConverterFunction, dbParams.ToArray());

            var mappedThread = new ThreadDetail
            {
                Id = thread.Id,
                PolitikerId = thread.Vorfall?.PolitikerId,
                PolitikerName = thread.Vorfall?.Politiker?.TitelName,
                Titel = thread.Title,
                VorfallId = thread.VorfallId,
                VorfallTitel = thread.Vorfall?.Titel,
                InitialPost = MapPost(posts.Single(n => n.Id == thread.InitialPostId), posts)
            };

            return mappedThread;
        }

        public void Like(ClaimsPrincipal user, long postId, LikeType? type)
        {
            var userId = _accountService.GetIdOfUser(user);

            if (!_context.Post.Any(n => n.Id == postId))
                throw new PostNotFoundException();

            if (type.HasValue)
            {
                var dbObj = _context.Like.SingleOrDefault(n => n.UserId == userId && n.PostId == postId);

                if (dbObj == null)
                {
                    _context.Add(new db.Like
                    {
                        UserId = userId,
                        PostId = postId,
                        LikeType = _mapper.Map<db.LikeType>(type),
                    });
                }
                else
                    dbObj.LikeType = _mapper.Map<db.LikeType>(type);
            }
            else
                _context.Remove(_context.Like.Single(n => n.UserId == userId && n.PostId == postId));

            _context.SaveChanges();
        }

        public Post Reply(ClaimsPrincipal user, PostData data)
        {
            var userId = _accountService.GetIdOfUser(user);

            if (!_context.Post.Any(n => n.Id == data.ParentId))
                throw new PostNotFoundException();

            var post = new db.Post()
            {
                Content = data.Content,
                Likes = new db.Like() { LikeType = db.LikeType.Like, UserId = userId }.AsList(),
                UserId = userId,
                ParentId = data.ParentId
            };

            _context.Add(post);
            _context.SaveChanges();

            return new Post
            {
                UserId = userId,
                Content = data.Content,
                CreatedOn = DateTime.Now,
                Dislikes = 0,
                Likes = 1,
                ParentId = data.ParentId,
                Id = post.Id,
                UserLike = LikeType.Like,
                UserName = _accountService.GetNameOfUser(user)
            };
        }

        public List<VorfallSelection> GetFilteredByName(string searchString, int take = 5)
        {
            var data = _context.Vorfall
                .Include(n => n.User)
                .Where(n => n.Titel.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                .Take(take)
                .Select(n => new VorfallSelection
                {
                    Id = n.Id,
                    User = n.User.UserName,
                    UserId = n.UserId,
                    Titel = n.Titel
                })
                .ToList();

            return data;
        }

        public SearchResult<ThreadOverview> Search(SearchParameters parameters, int pageSize = 25, int pageIndex = 0)
        {
            var builder = new StringBuilder();
            builder.Append(ThreadQuery);

            var dbParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter<string>("title", $"%{parameters.Titel ?? string.Empty}%"),
                new NpgsqlParameter<int>("limit", pageSize),
                new NpgsqlParameter<int>("offset", pageIndex * pageSize)
            };

            var countQuery = _context.Thread.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Titel))
            {
                countQuery = countQuery.Where(n => n.Title.Contains(parameters.Titel, StringComparison.CurrentCultureIgnoreCase));
            }

            var count = countQuery.Count();

            var data = this._context.GetData(builder.ToString(), n =>
            {
                var politikerName = string.Empty;

                var akadGradPre = n.GetString(8);
                if (!string.IsNullOrEmpty(akadGradPre))
                    politikerName = $"{akadGradPre} ";

                politikerName = $"{politikerName} {n.GetString(9)} {n.GetString(10)}";

                var akadGradPost = n.GetString(11);
                if (!string.IsNullOrEmpty(akadGradPost))
                    politikerName = $"{politikerName}, {akadGradPost}";

                var thread = new ThreadOverview()
                {
                    Id = n.GetInt64(0),
                    UserId = n.GetInt64(1),
                    VorfallId = n.GetInt64(2),
                    CreatedOn = n.GetDateTime(3),
                    Generated = n.GetBoolean(4),
                    PolitikerId = n.GetInt64(5),
                    Titel = n.GetString(6),
                    UserName = n.GetString(7),
                    PolitikerName = politikerName.Trim(),
                    VorfallTitel = n.GetString(12),
                    NumPosts = n.GetInt64(13)
                };

                if (thread.VorfallId == 0)
                {
                    thread.VorfallTitel = null;
                    thread.PolitikerName = null;
                }

                return thread;
            }, dbParams.ToArray());

            var result = new SearchResult<ThreadOverview>
            {
                MaxResults = count,
                Data = data.ToList()
            };

            return result;
        }

        private static Post MapPost(Post post, IEnumerable<Post> data)
        {
            var enumerable = data.ToList();
            var result = enumerable.Where(n => n.ParentId == post.Id).ToList();

            post.Children = result.Any() ? result : null;

            if (post.Children != null)
            {
                post.Children = post.Children.Select(n => MapPost(n, enumerable)).ToList();
            }

            return post;
        }


        #endregion Methods
    }
}
