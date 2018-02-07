using ANZ.Platform.App.EmailManager.Dal.Repositories;
using ANZ.Platform.App.EmailManager.Dal.UnitOfWork;
using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Signature.Services
{
    public class PublishServices
    {

        public void PublishTodaysPendingUpdates()
        {

            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
            UnitOfWork_EmployeeMaster UnitOfWork_EmpMaster = new UnitOfWork_EmployeeMaster();
            var itemsToPublish = UnitOfWork_App.EmailManager.GetOpenQueueDateNow();

            foreach (var item in itemsToPublish)
            {
                try {
                    var itemUpdateTypeId = item.UpdateTypeId;
                    var updateModel = StringSplitter(item);
                    var publishResult = UnitOfWork_EmpMaster.EmployeeMaster.PublishSignatureBanner(updateModel, itemUpdateTypeId);

                    if (publishResult.IsComplete == true)
                    {
                        updateModel.UsersUpdatedCount = publishResult.UpdateCount;
                        updateModel.UpdatedUserNames = publishResult.UpdatedUserNames;

                        if (itemUpdateTypeId == SignatureUpdateTypes.byPerson.GetHashCode())
                        {
                            UnitOfWork_App.EmailManager.SetAsPublished(updateModel);
                        }

                        else if (itemUpdateTypeId == SignatureUpdateTypes.byLocationDepatment.GetHashCode())
                        {
                            UnitOfWork_App.EmailManager.SetAsPublished(updateModel);
                        }

                        else if (itemUpdateTypeId == SignatureUpdateTypes.byPersonalAssistant.GetHashCode())
                        {
                            UnitOfWork_App.EmailManager.SetAsPublished(updateModel);
                        }

                        UnitOfWork_EmpMaster.Complete();
                        UnitOfWork_App.Complete();
                    }

                }

                catch (Exception e)
                { }
                    

            }


        }


        private SignatureUpdateQueueBusinessModel StringSplitter(SignatureUpdateQueueBusinessModel model)
        {

            string[] stringsToSplit = { model.LocationsToUpdateIds, model.DepartmentsToUpdateIds, model.UsersToUpdateIds, model.ManagersToUpdateIds, model.PersonalAssistantsToUpdateIds };

            int i;

            //foreach (var item in stringsToSplit[i])
            //{
            for (i=0; i < stringsToSplit.Length;)
                {
                    stringsToSplit[i] = RemoveLastIndexComma(stringsToSplit[i]);
                    i++;
                //}        
                
            }//end foreach


            model.LocationsToUpdateIds = stringsToSplit[0];
            model.DepartmentsToUpdateIds = stringsToSplit[1];
            model.UsersToUpdateIds = stringsToSplit[2];
            model.ManagersToUpdateIds = stringsToSplit[3];
            model.PersonalAssistantsToUpdateIds = stringsToSplit[4];
          

            return model;

        }



        private string RemoveLastIndexComma(string value)
        {
            if (value == null || value == "" || value == " ")
            {
                return value;
            }
                
            if (value != null || value != "" || value != " ")
            {
                value = value.Remove(value.Length - 1);
                return value;
            }


            return value;

        }


    }
}
