﻿
using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Core.Web.Base.Business
{
    public interface ICodeTabelValidator<T> where T : EntityBase
    {
        void CanUserInsert(T entity);
        void CanUserUpdate(T entity);
        void CanUserDelete(T entity);
    }
}
