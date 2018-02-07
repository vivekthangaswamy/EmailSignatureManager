using ANZ.Platform.App.EmailManager.Dal.UnitOfWork;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Signature.Services
{
    public class EmailManagerAnalyticsService
    {
        //return the api viewmodel
        public EmailmanagerAnalyticsDto EmailSignatureUpdateStatistics(int year)
        {
            var model = new EmailmanagerAnalyticsDto();
            var uow = new UnitOfWork_EmailManager();
            model.Published = uow.EmailManager.Analytics_GetPublishedUpdates(year);
            model.Created = uow.EmailManager.Analytics_GetCreatedUpdates(year);
            model.Cancelled = uow.EmailManager.Analytics_GetCancelledUpdates(year);
            model.All = uow.EmailManager.Analytics_GetCreatedUpdates(year);


            return model;

        }

        //return the date selection viewmodel to api
        public EmailmanagerAnalyticsDto GetDateSelectionFilter()
        {
            var model = new EmailmanagerAnalyticsDto();
            var uow = new UnitOfWork_EmailManager();
            model.All = uow.EmailManager.Analytics_GetAllUpdates();
            return model;

        }


    }
}
