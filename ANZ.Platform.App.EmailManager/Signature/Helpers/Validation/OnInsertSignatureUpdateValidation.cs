using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ValueTypes;


namespace ANZ.Platform.App.EmailManager.Signature.Helpers.Validation
{
    public class OnInsertSignatureUpdateValidation
    {
        //The method will need to be run before submitting any data from a form.
        //Given that form values will be used in the Where clasue - 
        //This will need to ensure that no blank/empty values are entered against the Where Clause.
        public bool ValidateForm(SignatureUpdateQueueBusinessModel entity)
        {
            var updateType = entity.UpdateTypeId;

            //Validate the "create signature update" by person selection Form
            if (updateType == SignatureUpdateTypes.byPerson.GetHashCode())
            {

                return  ValidateByPerson(entity);

            }

            //Validate the "create signature update" by Location and Department Form
            else if (updateType == SignatureUpdateTypes.byLocationDepatment.GetHashCode())
            {
                return ValidateLocationDepartment(entity);

            }


            //Validate the "Update Manager's personal Assistant" Form
            else if (updateType == SignatureUpdateTypes.byPersonalAssistant.GetHashCode())
            {

                return ValidateManagerPersonalAssistant(entity);
            }

            return false;

        }


        private bool ValidateByPerson(SignatureUpdateQueueBusinessModel entity)
        {
            if (entity.UsersToUpdateIds != null || entity.UsersToUpdateIds != "")
            {
                return true;
            }

            return false;

        }



        private bool ValidateLocationDepartment(SignatureUpdateQueueBusinessModel entity)
        {
            if (entity.DepartmentsToUpdateIds != null || entity.DepartmentsToUpdateIds != "" || entity.LocationsToUpdateIds != null || entity.LocationsToUpdateIds != "")
            {
                return true;
            }

            return false;

        }


        private bool ValidateManagerPersonalAssistant(SignatureUpdateQueueBusinessModel entity)
        {
            if (entity.ManagersToUpdateIds != null || entity.ManagersToUpdateIds != "" || entity.PersonalAssistantsToUpdateIds != null || entity.PersonalAssistantsToUpdateIds != "")
            {
                return true;
            }

            return false;

        }









    }
}
