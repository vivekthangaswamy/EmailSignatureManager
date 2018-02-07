using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.UtilityModels;
using System.Collections.Generic;
using System.ComponentModel;


namespace ANZ.Platform.App.EmailManager.Objects.ViewModels
{

    public class Signature_EditFormBase
    {

        public Signature_EditFormBase()
        {
          SignatureQueueUpdateItem = new SignatureUpdateQueueBusinessModel();
          



        }
        public SignatureUpdateQueueBusinessModel SignatureQueueUpdateItem { get; set; }
        public IEnumerable<EmployeeMasterBusinessModel> UsersToBeUpdated { get; set; }

    }

    public class Vm_SignatureForm_Form_UpdateByPerson
    {

        public IEnumerable<EmployeeMasterBusinessModel> Employee { get; set; }

    }


    [Description("Demonstrates DisplayNameAttribute.")]
    [DisplayName("EditEmailSignatureByLocationAndDepartment")]
    public class Vm_SignatureForm_Form_UpdateByLocationDepartment
    {

        public IEnumerable<LocationBusinessModel> Location { get; set; }
        public IEnumerable<DepartmentBusinessModel> Department { get; set; }

    }


    public class Vm_Signature_Form_UpdateManagerPersonalAssistant
    {
        public IEnumerable<Manager> Manager { get; set; }
        public IEnumerable<PersonalAssistant> PersonalAssistant { get; set; }


    }


    public class Vm_EditFormTemplate : Signature_EditFormBase
    {
        public IEnumerable<PeopleBusinessModel> Employee { get; set; }
        public IEnumerable<LocationBusinessModel> Location { get; set; }
        public IEnumerable<DepartmentBusinessModel> Department { get; set; }
        public IEnumerable<PeopleBusinessModel> Manager { get; set; }
        public IEnumerable<PeopleBusinessModel> PersonalAssistant { get; set; }

    }
}
