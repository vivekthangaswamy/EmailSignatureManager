using ANZ.Platform.App.EmailManager.Dal.Contexts;
using ANZ.Platform.App.EmailManager.Dal.Repositories;
using ANZ.Platform.App.EmailManager.Dal.Repositories.Interface;
using ANZ.Platform.App.EmailManager.Dal.UnitOfWork.Interface;
using System.Data.Entity;


namespace ANZ.Platform.App.EmailManager.Dal.UnitOfWork
{
    public class UnitOfWork_EmployeeMaster : IUnitOfWork_EmployeeMaster
    {
        private readonly DbContext _context;

        public UnitOfWork_EmployeeMaster()
        {
            _context = new EmployeeMasterContext();
            EmployeeMaster = new Repository_EmployeeMaster(_context);
        
        }

        public IRepository_EmployeeMaster EmployeeMaster { get; private set; }
     

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}
