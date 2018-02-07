using System;
using System.ComponentModel.DataAnnotations;


namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class SignatureImageBusinessModel
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageString { get; set; }
        public int ImageTypeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? LastModified { get; set; }
        public string Country { get; set; }


        public SignatureImageBusinessModel()
        {
            this.ImageTypeId = 1;
            this.IsActive = true;
            this.DateCreate = DateTime.UtcNow;
            this.LastModified = DateTime.UtcNow;

        }

    }
}
