using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
 

    public class PeopleBusinessModel
    {
        [Key]
        public double Id { get; set; }

        public string Description { get; set; }

        public string CIEquivalentTitle { get; set; }

        public float TermFlag { get; set; }

    }
}
