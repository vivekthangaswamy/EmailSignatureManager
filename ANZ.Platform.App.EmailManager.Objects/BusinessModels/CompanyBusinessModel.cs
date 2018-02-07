using System.ComponentModel.DataAnnotations;


namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class CompanyBusinessModel
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string LogoImgString { get; set; }
        public bool IsActive { get; set; }




    }
}
