using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;
using ANZ.Platform.App.EmailManager.Objects.UtilityModels;
using System.Collections.Generic;


namespace ANZ.Platform.App.EmailManager.Dal.Repositories.Interface
{
    public interface IRepository_EmployeeMaster
    {

        EmployeeMasterBusinessModel GetUserConfiguraation(string EmailAddress);
        IEnumerable<PeopleBusinessModel> GetAllEmployees(string Country);
        IEnumerable<LocationBusinessModel> GetAllLocations(string Country);
        IEnumerable<DepartmentBusinessModel> GetAllDepartments();
        IEnumerable<PeopleBusinessModel> GetManagers();
        IEnumerable<PeopleBusinessModel> GetPersonalAssistants();
        IEnumerable<EmployeeMasterBusinessModel> GetUsersToBeUpdated(string UserIds);
        PublishStoreProcedureResult PublishSignatureBanner(SignatureUpdateQueueBusinessModel model, int updateId);
        EmployeeMasterBusinessModel GetUser(int Id);
        void UpdateUserDetails(EmployeeMasterBusinessModel model);

    



    }
}
