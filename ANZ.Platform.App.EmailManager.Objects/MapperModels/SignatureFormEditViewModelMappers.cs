using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using System;


namespace ANZ.Platform.App.EmailManager.Objects.MapperModels
{
    public class SignatureFormEditViewModelMappers
    {

        public T MapSignatureFormEditViewModel<T>(SignatureUpdateQueueBusinessModel model)
        {
            var vm = new Signature_EditFormBase();


            if (model != null)
            {
               
                vm.SignatureQueueUpdateItem.UsersToUpdateIds = model.UsersToUpdateIds;
                vm.SignatureQueueUpdateItem.UpdateTypeId = model.UpdateTypeId;
                vm.SignatureQueueUpdateItem.UsersUpdatedCount = model.UsersUpdatedCount;
                vm.SignatureQueueUpdateItem.PublishDate = model.PublishDate;
                vm.SignatureQueueUpdateItem.MarketingImageBanner = model.MarketingImageBanner;
                vm.SignatureQueueUpdateItem.MarketingImageUrl = model.MarketingImageUrl;
               

               

            }

            else

            {

                //var emptyModel = vm.SignatureQueueUpdateItem.GetType().GetProperties();
                //foreach (var item in emptyModel)
                //{
                //    item.GetValue = "";

                //}


            }


            return (T)Convert.ChangeType(vm, typeof(T));

        }

        }

    }
