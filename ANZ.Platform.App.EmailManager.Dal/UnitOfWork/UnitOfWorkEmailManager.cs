using ANZ.Platform.App.EmailManager.Dal.Contexts;
using ANZ.Platform.App.EmailManager.Dal.Repositories;
using ANZ.Platform.App.EmailManager.Dal.Repositories.Interface;
using ANZ.Platform.App.EmailManager.Dal.UnitOfWork.Interface;
using System.Data.Entity;


namespace ANZ.Platform.App.EmailManager.Dal.UnitOfWork
{
    public class UnitOfWork_EmailManager : IUnitOfWork_EmailManager
    {
        private readonly DbContext _context;

        public UnitOfWork_EmailManager()
        {
            _context = new EmailManagerContext();

            EmailManager = new Repository_EmailManager(_context);
         
        }

        public IRepository_EmailManager EmailManager { get; private set; }
   

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
