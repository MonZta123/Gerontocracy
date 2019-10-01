﻿using System.Security.Claims;
using Gerontocracy.Core.BusinessObjects.Account;
using Gerontocracy.Core.BusinessObjects.Shared;
using Gerontocracy.Core.BusinessObjects.User;

namespace Gerontocracy.Core.Interfaces
{
    public interface IUserService
    {
        SearchResult<User> Search(SearchParameters parameters, int pageSize = 25, int pageIndex = 0);

        UserDetail GetUserDetail(long id);

        UserData GetUserPageData(long id);
        UserData GetUserPageData(ClaimsPrincipal user);
        UserData GetUserPageData(string name);
    }
}
