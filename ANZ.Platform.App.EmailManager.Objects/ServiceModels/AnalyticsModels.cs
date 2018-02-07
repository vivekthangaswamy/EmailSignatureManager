using System.Collections.Generic;
using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;

namespace ANZ.Platform.App.EmailManager.Objects.ServiceModels
{
    public class EmailmanagerAnalyticsDto
    {

        public IEnumerable<SignatureUpdateQueueBusinessModel> Published { get; set; }
        public IEnumerable<SignatureUpdateQueueBusinessModel> Created { get; set; }
        public IEnumerable<SignatureUpdateQueueBusinessModel> Cancelled { get; set; }
        public IEnumerable<SignatureUpdateQueueBusinessModel> All { get; set; }
        

    }
}
