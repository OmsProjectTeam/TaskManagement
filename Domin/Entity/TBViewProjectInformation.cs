using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewProjectInformation
	{
        public int IdProjectInformation { get; set; }
        public int IdProjectType { get; set; }
        public string ProjectTypes { get; set; }
        public string ProjectTypesAr { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectNameAr { get; set; }
        public string ProjectDescriptionAr { get; set; }
        public DateOnly ProjectStart { get; set; }
        public DateOnly ProjectEnd { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
