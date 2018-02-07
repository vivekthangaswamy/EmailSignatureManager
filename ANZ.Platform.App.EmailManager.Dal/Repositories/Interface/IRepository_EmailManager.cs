using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.Core.Objects.Database.EmailManager;
using System.Collections.Generic;


namespace ANZ.Platform.App.EmailManager.Dal.Repositories.Interface
{
    public interface IRepository_EmailManager
    {
        SignatureUpdateQueueBusinessModel GetQueueItem(int Id);
        IEnumerable<SignatureUpdateQueueBusinessModel> GetOpenQueue(RequestServiceModel model);
        IEnumerable<SignatureUpdateQueueBusinessModel> GetClosedQueue(RequestServiceModel model);
        IEnumerable<SignatureUpdateTypeBusinessModel> GetUpdateType();
        IEnumerable<CompanyBusinessModel> GetAllCompanies();
        void CreateUpdateRequest(SignatureUpdateQueueBusinessModel model);
        void EditUpdateRequest(SignatureUpdateQueueBusinessModel model);
        IEnumerable<SignatureImageBusinessModel> GetSignatureImageSelection();
        void CancelUpdateRequest(SignatureUpdateQueueBusinessModel model);
        IEnumerable<SignatureUpdateQueueBusinessModel> GetOpenQueueDateNow();
        void AddPhoto(SignatureImageBusinessModel model);
        void SetAsPublished(SignatureUpdateQueueBusinessModel model);
        IEnumerable<SignatureUpdateQueueBusinessModel> GetAllQueue(RequestServiceModel model);

        //Analytics
        IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetPublishedUpdates(int year);
        IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetCreatedUpdates(int year);
        IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetCancelledUpdates(int year);
        IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetAllUpdates();
    }
}
