
using ANZ.Platform.App.EmailManager.Dal.UnitOfWork;
using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ANZ.Platform.App.EmailManager.Signature.Services
{
    public class QueueServices
    {

        
        public Vm_SignatureHome GetHome(RequestServiceModel model)
        {

            var viewModel = new Vm_SignatureHome();
            UnitOfWork_EmailManager UnitOfWork = new UnitOfWork_EmailManager();

            viewModel.OpenQueue = UnitOfWork.EmailManager.GetOpenQueue(model);
            viewModel.ClosedQueue = UnitOfWork.EmailManager.GetClosedQueue(model);
            UnitOfWork.Complete();

            return viewModel;

        }



        public QueueViewModel GetAllQueue(RequestServiceModel model)
        {
            UnitOfWork_EmailManager UnitOfWork = new UnitOfWork_EmailManager();
            QueueViewModel result = new QueueViewModel();
            result.Queue = UnitOfWork.EmailManager.GetAllQueue(model);
            return result;
        }




        public bool CreateNewUpdateRequest(SignatureUpdateQueueBusinessModel model)
        {
            try
            {
                //Initialize Db Context
                UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
                UnitOfWork_App.EmailManager.CreateUpdateRequest(model);
                UnitOfWork_App.Complete();
                UnitOfWork_App.Dispose();

                return true;

            }

            catch (Exception e)
            { }

            return false;


        }

        public bool UpdateQueueItem(SignatureUpdateQueueBusinessModel model)
        {
            try
            {
                //Initialize Db Context
                UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
                UnitOfWork_App.EmailManager.EditUpdateRequest(model);
                UnitOfWork_App.Complete();
                UnitOfWork_App.Dispose();

                return true;

            }

            catch (Exception e)
            { }

            return false;

        }












    }
}
