using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.ViewModels
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Model name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Abrv is a required field.")]
        public string Abrv { get; set; }
        public string Make { get; set; }
        public int MakeId { get; set; }
    }
}
