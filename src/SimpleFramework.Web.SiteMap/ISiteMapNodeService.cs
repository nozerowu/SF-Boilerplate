﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFramework.Web.SiteMap
{
    public interface ISiteMapNodeService
    {
        Task<IEnumerable<ISiteMapNode>> GetSiteMapNodes(
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
