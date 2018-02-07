
using System.ComponentModel.DataAnnotations;

namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class SignatureUpdateTypeBusinessModel
    {
        [Key]
        public int UpdateTypeId { get; set; }
        public string Classification { get; set; }
        public string Description { get; set; }
        public string DisplayTitle { get; set; }
        public string DisplaySummary { get; set; }
        public string ImgString { get; set; }
        public string FormTemplate { get; set; }
        public bool IsActive { get; set; }


    }
}
