using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Objects.ViewModels
{
    public class AppManagerSignatureDashboardViewModel
    {
        public IEnumerable<SignatureUpdateQueueBusinessModel> Summary { get; set; }
        public IEnumerable<SignatureUpdateQueueBusinessModel> OpenQueue { get; set; }
        public IEnumerable<SignatureUpdateQueueBusinessModel> ClosedQueue { get; set; }
    }
}
