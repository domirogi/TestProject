using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Make name is a required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Abrv is a required field.")]
        public string Abrv { get; set; }
    }
}
