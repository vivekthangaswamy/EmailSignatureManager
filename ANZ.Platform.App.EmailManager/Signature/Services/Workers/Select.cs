using ANZ.Platform.App.EmailManager.Dal.UnitOfWork;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Signature.Services.Workers
{
    public class SelectActions
    {

        public Vm_Signature_GetFormTemplate FormOnload_GetByPerson(string Country, Vm_Signature_GetFormTemplate model, UnitOfWork_EmployeeMaster context)
        {
            
            model.Employee = context.EmployeeMaster.GetAllEmployees(Country);         
            return model;
        }



        public Vm_Signature_GetFormTemplate FormOnload_GetByDepartmentAndLocation(string Country, Vm_Signature_GetFormTemplate model, UnitOfWork_EmployeeMaster context)
        {
            model.Location = context.EmployeeMaster.GetAllLocations(Country);
            model.Department = context.EmployeeMaster.GetAllDepartments();
            return model;
        }


        public Vm_Signature_GetFormTemplate FormOnload_ManagerPA(Vm_Signature_GetFormTemplate model, UnitOfWork_EmployeeMaster context)
        {
            model.Manager = context.EmployeeMaster.GetManagers();
            model.PersonalAssistant = context.EmployeeMaster.GetPersonalAssistants();
            return model;

        }


        public Vm_SignatureHome GetQueueItem(RequestServiceModel model)
        {
            //Initialize Db Context.
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
            var viewModel = new Vm_SignatureHome();

            //Get both the open and closed queue.
            viewModel.OpenQueue = UnitOfWork_App.EmailManager.GetOpenQueue(model);
            viewModel.OpenQueue = UnitOfWork_App.EmailManager.GetClosedQueue(model);

            return viewModel;

        }






    }
}
