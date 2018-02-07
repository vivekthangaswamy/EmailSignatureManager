using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Objects.ServiceModels
{
    public class PublishStoreProcedureResult
    {

        public int UpdateCount { get; set; }
        public string UpdatedUserNames { get; set; }
        public bool IsComplete { get; set; }

    }
}
