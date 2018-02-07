using ANZ.Platform.App.EmailManager.Objects.ValueTypes;
using System;
using System.ComponentModel.DataAnnotations;


namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class SignatureUpdateQueueBusinessModel
    {

      

        [Key]
        public int Id { get; set; }

        //Used to determine the type of update being performed.
        public int UpdateTypeId { get; set; }

        //Seperate Colliers and PRD.
        public int CompanyId { get; set; }

        public string Title { get; set; }

        public DateTime? PublishDate { get; set; }

        public string MarketingImageBanner { get; set; }

        public string MarketingImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        
        public string LastModifiedBy { get; set; }

        //Used to collect the array of lcoation id's to update.
        public string LocationsToUpdateIds { get; set; }

        //Used to collect the array of department Id's to update.
        public string DepartmentsToUpdateIds { get; set; }

        //Used when updating Manager personal assistant(UserId's).
        public string ManagersToUpdateIds { get; set; }

        //Used when assigning PersonalAssistant Id's to Managers(UserId').
        public string PersonalAssistantsToUpdateIds { get; set; }

        //Used when updating individual email signatures.
        public string UsersToUpdateIds { get; set; }

        //Determie the status of the updated item eg pending, cancelled, error etc.
        public int TaskStatusId { get; set; }

        //The total of affected users.
        public int UsersUpdatedCount { get; set; }

        //record of usernames updated
        public string UpdatedUserNames { get; set; }

        public SignatureUpdateStatusBusinessModel UpdateStatus { get; set; }
        public CompanyBusinessModel Company { get; set; }



        public SignatureUpdateQueueBusinessModel()
        {


            this.CreatedDate = DateTime.UtcNow;
            this.LastModifiedDate = DateTime.UtcNow;
            this.TaskStatusId = SignatureQueueStatus.Pending.GetHashCode();


        }


    }



    
}
