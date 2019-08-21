﻿using System.Security.Claims;
using System.Threading.Tasks;

using Gerontocracy.Core.BusinessObjects.Account;
using Gerontocracy.Core.BusinessObjects.Shared;
using Gerontocracy.Core.BusinessObjects.Task;

namespace Gerontocracy.Core.Interfaces
{
    public interface ITaskService
    {
        SearchResult<AufgabeOverview> Search(SearchParameters parameters, int pageSize = 25, int pageIndex = 0);

        AufgabeDetail GetTask(long id);

        Task<User> AssignTask(ClaimsPrincipal user, long id);

        bool CloseTask(long id);

        bool ReopenTask(long id);
    }
}
