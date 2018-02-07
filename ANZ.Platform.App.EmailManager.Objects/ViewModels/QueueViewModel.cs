using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Objects.ViewModels
{
   public class QueueViewModel
   {
        public IEnumerable<SignatureUpdateQueueBusinessModel> Queue { get; set; }
    }
}
