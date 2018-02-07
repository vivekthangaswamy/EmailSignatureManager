using System.ComponentModel.DataAnnotations;

namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class DepartmentBusinessModel
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
        public string LongDescription { get; set; }

        public short IsActive { get; set; }

    }
}
