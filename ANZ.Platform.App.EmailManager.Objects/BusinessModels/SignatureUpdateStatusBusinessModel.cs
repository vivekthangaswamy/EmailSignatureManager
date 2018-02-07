using System.ComponentModel.DataAnnotations;


namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class SignatureUpdateStatusBusinessModel
    {
        [Key]
        public int TaskStatusId { get; set; }
        public string Name { get; set; }
      
    }
}
