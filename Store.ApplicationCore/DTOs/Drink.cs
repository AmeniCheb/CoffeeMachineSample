using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Store.ApplicationCore.Utils.CoffeeMachineEnum;

namespace Store.ApplicationCore.DTOs
{
    public class CreateDrinkRequest
    {
        [Required]
        public int Type { get; set; } 

        [Required]
        [Range(0, 5)]
        public int SugarQt { get; set; }

        [Required]
        public bool UseOwnMug { get; set; }
        
        public int ?  Badge { get; set; }
    }

    public class UpdateDrinkRequest : CreateDrinkRequest
    {
       
    }

    public class DrinkResponse
    {
        public int Id { get; set; }
        public int Type { get; set; } 
        public int SugarQt { get; set; }
        public bool UseOwnMug { get; set; }
        public int ?  Badge { set; get; }
    }
}
