using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class SignatureImageTypeBusinessModel
    {
        [Key]
        public int ImageTypeId { get; set; }
        public string ImageTypeName { get; set; }
        public ICollection<SignatureImageBusinessModel> SignatureImage { get; set; }

    }
}
