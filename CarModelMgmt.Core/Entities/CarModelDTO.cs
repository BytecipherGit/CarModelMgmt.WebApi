using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarModelMgmt.Core.Entities
{
    public class CarModelDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand name is required.")]
        public string Brand { get; set; }

      
        [Required(ErrorMessage = "Class name is required.")]
        public string? Class { get; set; }

        [Required(ErrorMessage = "Model name is required.")]
        public string ModelName { get; set; }
        [Required(ErrorMessage = "ModelCode is required.")]
        [MinLength(10, ErrorMessage = "ModelCode must be at least 10 characters long.")]

        public string ModelCode { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Features is required.")]
        
        public string Features { get; set; }

        [Required(ErrorMessage = "Price is required.")]     
        public double Price { get; set; }
        [Required(ErrorMessage = "Date of Manufacturing is required.")]

        public DateTime DateOfManufacturing { get; set; }
       
        public bool Active { get; set; }
        public int SortOrder { get; set; }
        public string ModelImage { get; set; }

    }
}
