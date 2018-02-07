using System.ComponentModel.DataAnnotations;


namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class LocationBusinessModel
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public string ZIP { get; set; }

        public string Country { get; set; }

        public byte IsActive { get; set; }

     

    }

}
