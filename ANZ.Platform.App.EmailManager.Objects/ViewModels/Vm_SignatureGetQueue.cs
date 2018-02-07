using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using System.Collections.Generic;


namespace ANZ.Platform.App.EmailManager.Objects.ViewModels
{
    public class Vm_SignatureHome
    {

        public IEnumerable<SignatureUpdateQueueBusinessModel> OpenQueue { get; set; }
        public IEnumerable<SignatureUpdateQueueBusinessModel> ClosedQueue { get; set; }
   


    }
}
