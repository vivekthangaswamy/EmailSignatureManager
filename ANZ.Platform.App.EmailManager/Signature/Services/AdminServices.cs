using ANZ.Platform.App.EmailManager.Dal.UnitOfWork;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Signature.Services
{
    public class AdminServices
    {
        public AppManagerSignatureDashboardViewModel GetAppManagerSignatureDashboard(RequestServiceModel model)
        {
            UnitOfWork_EmailManager unitOfWork_App = new UnitOfWork_EmailManager();
            AppManagerSignatureDashboardViewModel viewModel = new AppManagerSignatureDashboardViewModel();
            viewModel.Summary = unitOfWork_App.EmailManager.GetAllQueue(model);
            viewModel.OpenQueue = unitOfWork_App.EmailManager.GetOpenQueue(model);
            viewModel.ClosedQueue = unitOfWork_App.EmailManager.GetClosedQueue(model);
            unitOfWork_App.Complete();
            return viewModel;
        }

    }
}
