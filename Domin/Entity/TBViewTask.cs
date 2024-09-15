using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewTask
    {
        public int IdTask { get; set; }
        public int IdTaskStatus { get; set; }
        public string TaskStatus { get; set; }
        public int IdProjectInformation { get; set; }
        public string ProjectTypes { get; set; }
        public string ProjectTypesAr { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectNameAr { get; set; }
        public string ProjectDescriptionAr { get; set; }
        public DateOnly ProjectStart { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public DateTime? ActualEnd { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AddedBy { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public int IdTypesOfTask { get; set; }
        public string TypesOfTask { get; set; }
        public string TypesOfTaskAr { get; set; }
    }
}
