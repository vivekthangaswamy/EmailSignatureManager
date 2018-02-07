using ANZ.Platform.Core.EntityFramework;
using ANZ.Platform.Core.Objects.Database.EmailManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Dal.Repositories
{
    public partial class ApplicationBaseRepository: EfRepository
    {
        public ApplicationBaseRepository(DbContext context) : base(context)
        {

        }


        public SignatureUpdateQueue UpdatePublish(SignatureUpdateQueue model )
        {
            base.Context.Set<SignatureUpdateQueue>().Attach(model);
            base.Context.Entry(model).Property(m => m.TaskStatusId).IsModified = true;
            base.Context.Entry(model).Property(m => m.UsersUpdatedCount).IsModified = true;
            base.Context.Entry(model).Property(m => m.UpdatedUserNames).IsModified = true;

            return model;
        }


        public SignatureUpdateQueue UpdateCancellation(SignatureUpdateQueue model)
        {
            base.Context.Set<SignatureUpdateQueue>().Attach(model);
            base.Context.Entry(model).Property(m => m.TaskStatusId).IsModified = true;
  

            return model;
        }


        public string UpdatePartial <T>(T model, Array fieldsToUpdate) where T : class
        {
            var type = model.GetType();
            base.Context.Set<T>().Attach(model);

            foreach(var item in fieldsToUpdate)
            {
                List<string> splitFieldandValueArray = item.ToString().Split('=').ToList<string>();
                string fieldName = splitFieldandValueArray[0];
                var unformattedValue = splitFieldandValueArray[1];
                var property = type.GetProperty(fieldName);
                var propertyDataType = property.PropertyType;


                if (propertyDataType.Name.ToLower().Contains("int"))
                {                 
                    var formattedValue = Convert.ToInt32(unformattedValue);
                    property.SetValue(model, Convert.ToInt32(formattedValue));
                }

                else if (propertyDataType.Name.ToLower().Contains("string"))
                {
                    var formattedValue = unformattedValue.ToString();
                    property.SetValue(model, Convert.ToInt32(formattedValue));
                }


                else if (propertyDataType.Name.ToLower().Contains("float"))
                {
                    var formattedValue = unformattedValue.ToString();
                    property.SetValue(model, float.Parse(formattedValue));
                }



                base.Context.Entry(model).Property(property.Name).IsModified = true;

            }


            return null;
        }


        public class PartialUpdateBusinessModel
        {
            public string name { get; set; }

        }


        public class PartialUpdateBusinessModelList
        {
            public IEnumerable<PartialUpdateBusinessModel> PartialUpdateBusinessModelEnumerable { get; set; }

        }


    }


}

