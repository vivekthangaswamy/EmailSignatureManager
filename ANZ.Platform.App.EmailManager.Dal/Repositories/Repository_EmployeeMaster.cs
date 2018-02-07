using ANZ.Platform.Core.EntityFramework;
using ANZ.Platform.App.EmailManager.Dal.Repositories.Interface;
using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.UtilityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ANZ.Platform.Core.Objects.Database.EmployeeMaster;
using ANZ.Platform.App.EmailManager.Dal.AutoMappers;
using ANZ.Platform.Core.ObjectMapper;
using ANZ.Platform.Core.Objects.Database.EmailManager;
using ANZ.Platform.App.EmailManager.Dal.Contexts;
using System.Data.SqlClient;
using System.Data;
using ANZ.Platform.App.EmailManager.Objects.ServiceModels;

namespace ANZ.Platform.App.EmailManager.Dal.Repositories
{

    public class Repository_EmployeeMaster : EfRepository, IRepository_EmployeeMaster
    {
        
        private EntityMapper AutoMapper;
        private Automapper_EmployeeMaster CustomMapper;
       


        public Repository_EmployeeMaster(DbContext context) : base(context)
        {
            this.AutoMapper = new EntityMapper();
            this.CustomMapper = new Automapper_EmployeeMaster();
        }

        //Get the applications user details eg country etc as this can be used to return filtered data
        public EmployeeMasterBusinessModel GetUserConfiguraation(string EmailAddress)
        {
            var data = Query<EmployeeMasterList>().First(x => x.email == EmailAddress);
            return AutoMapper.MaptoObject<EmployeeMasterList, EmployeeMasterBusinessModel>(data);

        }

        //----------------------------------------------------
        // Form controls - Dropdown box etc
        //----------------------------------------------------   

        public IEnumerable<LocationBusinessModel> GetAllLocations(string Country)
        {
            var data  = Query<Location>().Where(x => x.IsActive == 1).Where(x=>x.Country == Country);
            return AutoMapper.MaptoIEnumerableObject<Location, LocationBusinessModel>(data);

        }


        public IEnumerable<DepartmentBusinessModel> GetAllDepartments()
        {
            var data =  Query<Department>().Where(x => x.IsActive == 1);
            return AutoMapper.MaptoIEnumerableObject<Department, DepartmentBusinessModel>(data);
        }


        public IEnumerable<PeopleBusinessModel> GetAllEmployees(string Country)
        {
            Automapper_EmployeeMaster cs = new Automapper_EmployeeMaster();
            var data = Query<EmployeeMasterList>().Where(x => x.Termflag == 0).Where(x=>x.Location.Country == Country);
            return CustomMapper.MapIEnumerablePeopleObject<EmployeeMasterList, PeopleBusinessModel>(data);

        }




        public IEnumerable<PeopleBusinessModel> GetManagers()
        {
            Automapper_EmployeeMaster cs = new Automapper_EmployeeMaster();
            var data = Query<EmployeeMasterList>().Where(x => x.Termflag !=0).Where(x=>x.CIEquivalentTitle.Contains("Director"));
            return CustomMapper.MapIEnumerablePeopleObject<EmployeeMasterList, PeopleBusinessModel>(data);
        }


        public IEnumerable<PeopleBusinessModel> GetPersonalAssistants()
        {
            Automapper_EmployeeMaster cs = new Automapper_EmployeeMaster();
            var data = Query<EmployeeMasterList>().Where(x => x.Termflag != 0).Where(x=>x.CIEquivalentTitle.Contains("PA"));
            return CustomMapper.MapIEnumerablePeopleObject<EmployeeMasterList, PeopleBusinessModel>(data);
        }


     

        //----------------------------------------------------
        // Users
        //---------------------------------------------------- 


        public IEnumerable<EmployeeMasterBusinessModel> GetUsersToBeUpdated(string userIds)
        {
            try
            {

                List<string> Ids = userIds.Split(',').ToList<string>();
                double[] formattedIds = new double[Ids.Count()];

                int i = 0;
                foreach (var item in Ids)
                {
                    formattedIds[i] = Convert.ToDouble(item);

                    i++;
                }

                var data =  Query<EmployeeMasterList>()
                    .Where(x => formattedIds.Contains(x.Id))
                    .Include(c => c.Location)
                    .Include(x => x.Department).ToList();

                return AutoMapper.MaptoIEnumerableObject<EmployeeMasterList, EmployeeMasterBusinessModel>(data);

            }

            catch (Exception e)
            {

                return null;
            }



        }// End Method GetUsersToBeUpdated(string userIds)


        public EmployeeMasterBusinessModel GetUser(int Id)
        {
            var data = Get<EmployeeMasterList>(Id);
            return CustomMapper.MapEmployeeMasterList<EmployeeMasterList, EmployeeMasterBusinessModel>(data);
        }

        public void UpdateUserDetails(EmployeeMasterBusinessModel model)
        {

            var entity = CustomMapper.MapEmployeeMasterList<EmployeeMasterBusinessModel, EmployeeMasterList>(model);
            List<string> fieldsToUpdate = new List<string>();
            fieldsToUpdate.Add("CIEquivalentTitle=" + model.CIEquivalentTitle);
            fieldsToUpdate.Add("DisplayDepartment=" + entity.DisplayDepartment);
            fieldsToUpdate.Add("IsEnableMarketingImage=" + entity.IsEnableMarketingImage);
            fieldsToUpdate.Add("IsUpdated=" + 1);
            var array = fieldsToUpdate.ToArray();

            UpdatePartial<EmployeeMasterList>(entity, array);

        }

        //----------------------------------------------------
        // Signture Update Publishidng 
        //---------------------------------------------------- 



        public void UpdateBannerByPerson(SignatureUpdateQueueBusinessModel model)
        {

            var usersToUpdate = model.UsersToUpdateIds;
          

            if (usersToUpdate != null || usersToUpdate != "")
            { usersToUpdate = usersToUpdate.Remove(model.LocationsToUpdateIds.Length - 1); }

           

            var usersToUpdateParameter = new SqlParameter("@LocationIds", usersToUpdate);
  
            var conn = new SqlConnection(Context.Database.Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand("dbo.SP_ANZ_App_EmailManager_Signature_UpdateBannerbyLocationDepartment", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserIds", usersToUpdateParameter));
      
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }



        public PublishStoreProcedureResult PublishSignatureBanner(SignatureUpdateQueueBusinessModel model, int UpdateType)
        {

            var locationIds = model.LocationsToUpdateIds;
            var departmentIds = model.DepartmentsToUpdateIds;
            var userIds = model.UsersToUpdateIds;
            var managerIds = model.ManagersToUpdateIds;
            var personalAssistantIds = model.PersonalAssistantsToUpdateIds;
            var bannerImage = model.MarketingImageBanner;
            var bannerUrl = model.MarketingImageUrl;

            var conn = new SqlConnection(Context.Database.Connection.ConnectionString);
            var result = new PublishStoreProcedureResult();

            try
            {
                SqlCommand cmd = new SqlCommand("dbo.SP_ANZ_App_Emailmanager_PublishUpdates", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UpdateType", UpdateType));
                cmd.Parameters.Add(new SqlParameter("@LocationIds", locationIds));
                cmd.Parameters.Add(new SqlParameter("@DepartmentIds", departmentIds));
                cmd.Parameters.Add(new SqlParameter("@UserIds", userIds));
                cmd.Parameters.Add(new SqlParameter("@ImageString", bannerImage));
                cmd.Parameters.Add(new SqlParameter("@ImageUrl", bannerUrl));
                SqlParameter updateCount = cmd.Parameters.Add("@UpdateCount", SqlDbType.Int);
                SqlParameter updatedUserNames = cmd.Parameters.Add("@UpdatedUserNames", SqlDbType.NVarChar);
                updateCount.Direction = ParameterDirection.Output;
                updateCount.Size = 6;
                updatedUserNames.Direction = ParameterDirection.Output;
                updatedUserNames.Size = 10000;

                conn.Open();
                cmd.ExecuteNonQuery();

                result.UpdateCount = (int)cmd.Parameters["@UpdateCount"].Value;
                result.UpdatedUserNames = (string)cmd.Parameters["@UpdatedUserNames"].Value;
                result.IsComplete = true;

                return result;


            }

            catch (Exception e)
            {
                result.IsComplete = false;
                return result;
            }

            finally          
            {
                conn.Close();
            }

           
        } //end function public bool PublishSignatureBanner    

    }
}
