using ANZ.Platform.App.EmailManager.Dal.Repositories.Interface;
using System;


namespace ANZ.Platform.App.EmailManager.Dal.UnitOfWork.Interface
{
    public interface IUnitOfWork_EmployeeMaster : IDisposable
    {
        IRepository_EmployeeMaster EmployeeMaster { get; }

        int Complete();
    }
}
