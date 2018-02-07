using ANZ.Platform.App.EmailManager.Signature.Helpers.Validation;
using ANZ.Platform.App.EmailManager.Dal.UnitOfWork;
using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.MapperModels;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.ValueTypes;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using AutoMapper;
using System;
using ANZ.Platform.App.EmailManager.Signature.Services.Workers;
using ANZ.Platform.App.EmailManager.Dal.Repositories;
using System.Collections.Generic;

namespace ANZ.Platform.App.EmailManager.Signature.Services
{
    public class FormServices
    {
        public EmployeeMasterBusinessModel GetUserConfiguration(string EmailAddress)
        {
            UnitOfWork_EmployeeMaster UnitOfWork_Emp = new UnitOfWork_EmployeeMaster();
            var result = UnitOfWork_Emp.EmployeeMaster.GetUserConfiguraation(EmailAddress);
            UnitOfWork_Emp.Complete();
            return result;

        }
     
        public string CreateNewSignatureUpdate(SignatureUpdateQueueBusinessModel form)
        {

            try
            {
                var formValidation = new OnInsertSignatureUpdateValidation();
        
                if (formValidation.ValidateForm(form) == true)
                {
                    InsertUpdateIntoDB(form);
                    return ServiceValidationStatus.Success;
                }

                return ServiceValidationStatus.Rejected;

            }//End try
            catch (Exception e)
            {
                return ServiceValidationStatus.Error + " -" + e;
            }//End catch
        }//End Method: CreateNewSignatureUpdate


        public string EditSignatureUpdate(SignatureUpdateQueueBusinessModel form)
        {

            try
            {

                var formValidation = new OnInsertSignatureUpdateValidation();

                if (formValidation.ValidateForm(form) == true)
                {
                    EditUpdateRequest(form);             
                    return ServiceValidationStatus.Success;
                }

                return ServiceValidationStatus.Rejected;

            }

            catch (Exception e)
            { return ServiceValidationStatus.Error + " -" + e; }

        }


        public void EditUpdateRequest(SignatureUpdateQueueBusinessModel form)
        {
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
            UnitOfWork_App.EmailManager.EditUpdateRequest(form);
            UnitOfWork_App.Complete();
        }


        public void InsertUpdateIntoDB(SignatureUpdateQueueBusinessModel model)
        {
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
            UnitOfWork_App.EmailManager.CreateUpdateRequest(model);
            UnitOfWork_App.Complete();
        }


        // This Service Controller Get the the form viemodel (to populate controls) when creating the viewmodel.
        // As each form may display dirrent controls, the viewmodel served will need to be relevant
        // to the form.

        // Initialize the unitof work context. 
        // This wil be inject into the service methods.
        // Once results have been executed, the service controller will dispose of the context.
        public Vm_Signature_GetFormTemplate GetPrePopulatedControlItems(string Country, int FormTypeId)
        {
         
            var formTemplate = new Vm_Signature_GetFormTemplate();
            var worker = new SelectActions();
            var UnitOfWorkEmployeeMaster = new UnitOfWork_EmployeeMaster();

            if (FormTypeId == SignatureUpdateTypes.byPerson.GetHashCode())
            {
                return worker.FormOnload_GetByPerson(Country, formTemplate, UnitOfWorkEmployeeMaster);
            }
            else if (FormTypeId == SignatureUpdateTypes.byLocationDepatment.GetHashCode())
            {
                return worker.FormOnload_GetByDepartmentAndLocation(Country, formTemplate, UnitOfWorkEmployeeMaster);
            }
            else if (FormTypeId == SignatureUpdateTypes.byPersonalAssistant.GetHashCode())
            {
                return worker.FormOnload_ManagerPA(formTemplate, UnitOfWorkEmployeeMaster);
            }


            return null;

        }


        public Vm_Signature_GetFormTemplate GetExistingItem(string Country, int itemId)
        {
            var query = new UnitOfWork_EmailManager();
            var selectedItem = query.EmailManager.GetQueueItem(itemId);
            var formTemplate = GetPrePopulatedControlItems(Country, selectedItem.UpdateTypeId);
            formTemplate.ExistingItem = selectedItem;
            return formTemplate;


        }

        public Vm_EditFormTemplate EditTemplate<T>(GetItemRequest model)
        {
            if (model.ItemType == SignatureUpdateTypes.byPerson.GetHashCode())
            {
                return GetFormEditView_ByPersonType(model);

            }

            else if (model.ItemType == SignatureUpdateTypes.byLocationDepatment.GetHashCode())
            {
                return GetFormEditView_ByLocationDepartmentType(model);

            }

            else if (model.ItemType == SignatureUpdateTypes.byPersonalAssistant.GetHashCode())
            {
                return GetFormEditView_ByPersonalAssistant(model);

            }

            return null;

        }



        public Vm_EditFormTemplate GetFormEditView_ByPersonType(GetItemRequest model)
        {
            var viewModel = new Vm_EditFormTemplate();

            //Initialize Db Context
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();

            //Get the queue item details
            var queueItem = UnitOfWork_App.EmailManager.GetQueueItem(model.Id);

            //Map the queue view model to the DTO
            viewModel.SignatureQueueUpdateItem = GenericEditViewModelMapper(queueItem);

            //execute the context
            UnitOfWork_App.Complete();
            UnitOfWork_App.Dispose();

            UnitOfWork_EmployeeMaster UnitOfWork_Emp = new UnitOfWork_EmployeeMaster();

            //Populate Form dropdown lists
            viewModel.Employee = UnitOfWork_Emp.EmployeeMaster.GetAllEmployees("australia");

            ////Get the preview of users to be updated.
            ////Inject the current context into the method to for use.
            viewModel.UsersToBeUpdated = UnitOfWork_Emp.EmployeeMaster.GetUsersToBeUpdated(queueItem.UsersToUpdateIds);

            UnitOfWork_Emp.Complete();
            UnitOfWork_Emp.Dispose();

            return viewModel;


        }


        public Vm_EditFormTemplate GetFormEditView_ByLocationDepartmentType(GetItemRequest model)
        {

            var viewModel = new Vm_EditFormTemplate();

            //Initialize Db Context
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();

            //Get the queue item details
            var queueItem = UnitOfWork_App.EmailManager.GetQueueItem(model.Id);

            //Map the queue view model to the DTO
            viewModel.SignatureQueueUpdateItem = GenericEditViewModelMapper(queueItem);

            //execute the context
            UnitOfWork_App.Complete();
            UnitOfWork_App.Dispose();

            UnitOfWork_EmployeeMaster UnitOfWork_Emp = new UnitOfWork_EmployeeMaster();

            //Populate Form dropdown lists
            viewModel.Location = UnitOfWork_Emp.EmployeeMaster.GetAllLocations("australia");
            viewModel.Department = UnitOfWork_Emp.EmployeeMaster.GetAllDepartments();

            ////Get the preview of users to be updated.
            ////Inject the current context into the method to for use.
            viewModel.UsersToBeUpdated = UnitOfWork_Emp.EmployeeMaster.GetUsersToBeUpdated(queueItem.UsersToUpdateIds);

            //Use the below helper to manaully map queue Location & Department by code
            //--var helper = new UsersToBeUpdatedHelper();
            //viewModel.UsersToBeUpdated = helper.ManageUsersToBeUpdated(queueItem.UserId, UnitOfWork_Emp);

            UnitOfWork_Emp.Complete();

            return viewModel;


        }




        public Vm_EditFormTemplate GetFormEditView_ByPersonalAssistant(GetItemRequest model)
        {
            var viewModel = new Vm_EditFormTemplate();


            //Initialize Db Context
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();

            //Get the queue item details
            var queueItem = UnitOfWork_App.EmailManager.GetQueueItem(model.Id);

            //Map the queue view model to the DTO
            viewModel.SignatureQueueUpdateItem = GenericEditViewModelMapper(queueItem);

            //execute the context
            UnitOfWork_App.Complete();
            UnitOfWork_App.Dispose();

            UnitOfWork_EmployeeMaster UnitOfWork_Emp = new UnitOfWork_EmployeeMaster();

            //Populate Form dropdown lists
            viewModel.Manager = UnitOfWork_Emp.EmployeeMaster.GetManagers();
            viewModel.PersonalAssistant = UnitOfWork_Emp.EmployeeMaster.GetPersonalAssistants();

            ////Get the preview of users to be updated.
            ////Inject the current context into the method to for use.
            viewModel.UsersToBeUpdated = UnitOfWork_Emp.EmployeeMaster.GetUsersToBeUpdated(queueItem.UsersToUpdateIds);


            return null;
        }



        public void CancelUpdateRequest(SignatureUpdateQueueBusinessModel model)
        {
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
            UnitOfWork_App.EmailManager.CancelUpdateRequest(model);
            UnitOfWork_App.Complete();


        }


        //Map the queue view model to the DTO
        public SignatureUpdateQueueBusinessModel GenericEditViewModelMapper(SignatureUpdateQueueBusinessModel queueItem)
        {
            if (queueItem != null)
            {

                var mapper = new SignatureFormEditViewModelMappers();
                var mapped = mapper.MapSignatureFormEditViewModel<Signature_EditFormBase>(queueItem);

                return Mapper.Map<SignatureUpdateQueueBusinessModel>(mapped.SignatureQueueUpdateItem);

            }

            return null;

        }


        public EmployeeListViewModel GetUsers()
        {
            UnitOfWork_EmployeeMaster UnitOfWork_Emp = new UnitOfWork_EmployeeMaster();
            EmployeeListViewModel viewModel = new EmployeeListViewModel();
            viewModel.employees = UnitOfWork_Emp.EmployeeMaster.GetAllEmployees("australia");
            UnitOfWork_Emp.Complete();
            return viewModel;
        }

        public EmployeeViewModel GetUser(int Id)
        {
            UnitOfWork_EmployeeMaster UnitOfWork_Emp = new UnitOfWork_EmployeeMaster();           
            var result = UnitOfWork_Emp.EmployeeMaster.GetUser(Id);
            UnitOfWork_Emp.Complete();
            EmployeeViewModel viewModel = new EmployeeViewModel();
            viewModel.EmployeeDetails = result;
            return viewModel;
        }


        public void UpdatedSignatureDetails(EmployeeMasterBusinessModel model)
        {
            UnitOfWork_EmployeeMaster UnitOfWork_Emp = new UnitOfWork_EmployeeMaster();
            UnitOfWork_Emp.EmployeeMaster.UpdateUserDetails(model);
            UnitOfWork_Emp.Complete();
        }








    }
}
