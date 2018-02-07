using System.ComponentModel.DataAnnotations;


namespace ANZ.Platform.App.EmailManager.Objects.ServiceModels
{
    public class GetItemRequest
    {
        [Required]
        public int Id { get; set; }
        public int ItemType { get; set; }
        public RequestServiceModel ServiceModel {get;set;}

       

    }
}
