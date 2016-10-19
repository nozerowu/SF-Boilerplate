﻿using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Services
{
    public interface IUrlSlugService
    {
        UrlSlugEntity Get(long entityId, long entityTypeId);

        void Add(string name, long entityId, long entityTypeId);

        void Update(string newName, long entityId, long entityTypeId);

        void Remove(long entityId, long entityTypeId);
    }
}
