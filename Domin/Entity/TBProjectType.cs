using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBProjectType
    {
        [Key]
        public int IdProjectType { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlProjectTypes")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength300")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string ProjectTypes { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlProjectTypesAr")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength300")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string ProjectTypesAr { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public bool Active { get; set; }
    }
}
