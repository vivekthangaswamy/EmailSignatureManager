using ANZ.Platform.App.EmailManager.Dal.Repositories.Interface;
using System;


namespace ANZ.Platform.App.EmailManager.Dal.UnitOfWork.Interface
{
    public interface IUnitOfWork_EmailManager : IDisposable
    {
        IRepository_EmailManager EmailManager { get; }

        int Complete();
    }
}
