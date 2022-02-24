using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Store.ApplicationCore.Entities
{
    public class Drink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Type { get; set; }
        public int SugarQt { get; set; }
        public bool UseOwnMug { get; set; }
        public int ? Badge { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
