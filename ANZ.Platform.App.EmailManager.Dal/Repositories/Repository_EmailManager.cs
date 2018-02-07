using ANZ.Platform.Core.EntityFramework;
using ANZ.Platform.App.EmailManager.Dal.Repositories.Interface;
using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.ValueTypes;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ANZ.Platform.Core.Objects.Database.EmailManager;
using ANZ.Platform.Core.ObjectMapper;
using ANZ.Platform.App.EmailManager.Dal.AutoMappers;
using System;


namespace ANZ.Platform.App.EmailManager.Dal.Repositories
{
    public class Repository_EmailManager : EfRepository, IRepository_EmailManager
    {
        private EntityMapper AutoMapper;
        private AutoMapper_Emailmanager CustomMapper;
        public Repository_EmailManager(DbContext context) : base(context)
        {
            this.AutoMapper = new EntityMapper();
            this.CustomMapper = new AutoMapper_Emailmanager();
        }

        //----------------------------------------------------
        // Email Queue
        //----------------------------------------------------

        public IEnumerable<SignatureUpdateQueueBusinessModel> GetOpenQueue(RequestServiceModel model)
        {
            var data =  Query<SignatureUpdateQueue>().Where(x => x.TaskStatusId == (int)SignatureQueueStatus.Pending).Include(c => c.UpdateStatus).Include(c => c.Company).OrderByDescending(x=>x.PublishDate);
            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }


        public IEnumerable<SignatureUpdateQueueBusinessModel> GetClosedQueue(RequestServiceModel model)
        {
           var data = Query<SignatureUpdateQueue>().Where(x => x.TaskStatusId == (int)SignatureQueueStatus.Published).Include(c => c.UpdateStatus).Include(c => c.Company).OrderByDescending(x => x.PublishDate).Take(15); 
            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }



        public IEnumerable<SignatureUpdateQueueBusinessModel> GetAllQueue(RequestServiceModel model)
        {
            var data = Query<SignatureUpdateQueue>().Include(c => c.UpdateStatus).Include(c => c.Company).OrderByDescending(x => x.CreatedDate).Take(100); 
            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }


        //----------------------------------------------------
        // Form types
        //----------------------------------------------------

        public IEnumerable<SignatureUpdateTypeBusinessModel> GetUpdateType()
        {
            var data =  Query<SignatureUpdateType>().Where(x => x.IsActive == true);
            return AutoMapper.MaptoIEnumerableObject<SignatureUpdateType, SignatureUpdateTypeBusinessModel>(data);
        }


        //----------------------------------------------------
        // Form Controls
        //----------------------------------------------------

        public IEnumerable<CompanyBusinessModel> GetAllCompanies()
        {
            var data =  Query<Company>().Where(x => x.IsActive == true);
            return AutoMapper.MaptoIEnumerableObject<Company, CompanyBusinessModel>(data);

        }


        //----------------------------------------------------
        // Form Actions
        //----------------------------------------------------


        public SignatureUpdateQueueBusinessModel GetQueueItem(int itemId)
        {
            var data =  Get<SignatureUpdateQueue>(itemId);
            return CustomMapper.MapSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }



        public void CreateUpdateRequest(SignatureUpdateQueueBusinessModel data)
        {
            AutoMapper_Emailmanager cs = new AutoMapper_Emailmanager();
            var entity = CustomMapper.MapSignatureUpdateObject<SignatureUpdateQueueBusinessModel, SignatureUpdateQueue>(data);
            Add(entity);
           
        }


        public void EditUpdateRequest(SignatureUpdateQueueBusinessModel model)
        {

            var entity = CustomMapper.MapSignatureUpdateObject<SignatureUpdateQueueBusinessModel, SignatureUpdateQueue>(model);
            Update(entity);

        }

        public void CancelUpdateRequest(SignatureUpdateQueueBusinessModel model)
        {         
            var entity = CustomMapper.MapSignatureUpdateObject<SignatureUpdateQueueBusinessModel, SignatureUpdateQueue>(model);      
            List<string> fieldsToUpdate = new List<string>();
            fieldsToUpdate.Add("TaskStatusId=" + SignatureQueueStatus.Cancelled.GetHashCode());
            var array = fieldsToUpdate.ToArray();

            UpdatePartial<SignatureUpdateQueue>(entity, array);
  
        }


        //----------------------------------------------------
        // Images
        //----------------------------------------------------


        public IEnumerable<SignatureImageBusinessModel> GetSignatureImageSelection()
        {
            var data =  Query<SignatureImage>().OrderByDescending(x => x.DateCreate); 
            return AutoMapper.MaptoIEnumerableObject<SignatureImage, SignatureImageBusinessModel>(data);
        }



        public void AddPhoto(SignatureImageBusinessModel model)
        {
            var entity =  AutoMapper.MaptoObject<SignatureImageBusinessModel, SignatureImage>(model);
            Add(entity);
        }




        //----------------------------------------------------
        // Publishing
        //----------------------------------------------------


        public IEnumerable<SignatureUpdateQueueBusinessModel> GetOpenQueueDateNow()
        {
            PublishingMappers mapper = new PublishingMappers();
            var today = DateTime.Now.AddDays(1);

                var data =  Query<SignatureUpdateQueue>().AsNoTracking().Where(x => x.TaskStatusId == (int)SignatureQueueStatus.Pending).Where(m=>m.PublishDate <= today);

            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data); 

        }


        public void SetAsPublished(SignatureUpdateQueueBusinessModel model)
        {
     
            var entity = CustomMapper.MapSignatureUpdateObject<SignatureUpdateQueueBusinessModel, SignatureUpdateQueue>(model);
            List<string> fieldsToUpdate = new List<string>();
            fieldsToUpdate.Add("TaskStatusId=" + SignatureQueueStatus.Published.GetHashCode());
            fieldsToUpdate.Add("UsersUpdatedCount=" + entity.UsersUpdatedCount);
            fieldsToUpdate.Add("UpdatedUserNames=" + entity.UpdatedUserNames);
            var array = fieldsToUpdate.ToArray();

            UpdatePartial<SignatureUpdateQueue>(entity, array);

        }



        //----------------------------------------------------
        // Analytics
        //----------------------------------------------------


        //Get all the available years to be displayed as selection on anaytics filter.
        public IEnumerable<SignatureUpdateQueueBusinessModel> GetDateSelection()
        {
            var data = Query<SignatureUpdateQueue>();
            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);


        }



        //SECTION 1 -- Group email signatures by status IE published, created & cancelled || Chart of Email Signature Analytics dashboard

        public IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetPublishedUpdates(int year)
        {

            var data = Query<SignatureUpdateQueue>()
                .Where(x => x.PublishDate.Value.Year == year)
                .Where(x => x.TaskStatusId == 3);

            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }


        public IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetCreatedUpdates(int year)
        {

            var data = Query<SignatureUpdateQueue>()
                .Where(x => x.CreatedDate.Value.Year == year);

            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }


        public IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetCancelledUpdates(int year)
        {

            var data = Query<SignatureUpdateQueue>()
                .Where(x => x.CreatedDate.Value.Year == year)
                .Where(x => x.TaskStatusId == 4);

            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }


        public IEnumerable<SignatureUpdateQueueBusinessModel> Analytics_GetAllUpdates()
        {

            var data = Query<SignatureUpdateQueue>();
            return CustomMapper.MapIEnumerableSignatureUpdateObject<SignatureUpdateQueue, SignatureUpdateQueueBusinessModel>(data);
        }





    }
}
