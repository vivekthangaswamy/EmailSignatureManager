

namespace ANZ.Platform.App.EmailManager.Objects.ServiceModels
{
    //*FormTypeId only populated if the incoming request is to 'create' new item. Default value to be '0'
    //*ExistingItemId only popULted if the incoming request is to update an existing update item.Default value to be '0'


    public class GetFormRequest
    {

        public int FormTypeId { get; set; }

        public int ExistingItemId { get; set; }

    }
}
