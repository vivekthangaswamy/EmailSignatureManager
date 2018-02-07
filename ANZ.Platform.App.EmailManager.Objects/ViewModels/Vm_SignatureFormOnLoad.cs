using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.UtilityModels;
using System.Collections.Generic;

namespace ANZ.Platform.App.EmailManager.Objects.ViewModels
{




    public class Vm_SignatureForm_OnLoad_UpdateByPerson 
    {

        public IEnumerable<Employee> Employee { get; set; }

    }


    public class Vm_SignatureForm_OnLoad_UpdateByLocationDepartment 
    {

        public IEnumerable<LocationBusinessModel> Location { get; set; }
        public IEnumerable<LocationBusinessModel> Department { get; set; }

    }


    public class Vm_Signature_OnLoad_UpdateManagerPersonalAssistant
    {
        public IEnumerable<Manager> Manager { get; set; }
        public IEnumerable<PersonalAssistant> PersonalAssistant { get; set; }


    }



    public class Vm_Signature_GetFormTemplate
    {

        public IEnumerable<PeopleBusinessModel> Employee { get; set; }
        public IEnumerable<LocationBusinessModel> Location { get; set; }
        public IEnumerable<DepartmentBusinessModel> Department { get; set; }
        public IEnumerable<PeopleBusinessModel> Manager { get; set; }
        public IEnumerable<PeopleBusinessModel> PersonalAssistant { get; set; }
        public SignatureUpdateQueueBusinessModel ExistingItem { get; set; }
        public RequestServiceModel RequestModel { get; set; }

    }

}
