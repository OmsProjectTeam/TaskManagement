using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewRequestsTask
    {
        public int IdRequestsTask { get; set; }
        public int IdTask { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNameAr { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public string? Photo { get; set; }
        public string RequestsAr { get; set; }
        public string RequestsEn { get; set; }
        public string AddedBy { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public string RequestsTitelEn { get; set; }
        public string RequestsTitelAr { get; set; }

    }
}
