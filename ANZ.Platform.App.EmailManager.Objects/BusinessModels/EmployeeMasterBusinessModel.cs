using System.ComponentModel.DataAnnotations;


namespace ANZ.Platform.App.EmailManager.Objects.BusinessModels
{
    public class EmployeeMasterBusinessModel
    {
        [Key]
        public int Id { get; set; }
        public string PrefName { get; set; }
        public string MGMNT1 { get; set; }
        public string MGMNT2 { get; set; }
        public string MGMNT3 { get; set; }
        public string MGMNT4 { get; set; }
        public string CIEquivalentTitle { get; set; }
        public string email { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public byte DisplayDepartment { get; set; }
        public byte isUpdated { get; set; }
        public string MarketingImagePath { get; set; }
        public string MarketingUrl { get; set; }
        public byte? IsEnableMarketingImage { get; set; }
        public short Termflag { get; set; }
        public LocationBusinessModel Location { get; set; }
        public DepartmentBusinessModel Department { get; set; }
        public EmployeeMasterBusinessModel()
        {


        }


    }
}
