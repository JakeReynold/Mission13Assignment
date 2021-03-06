using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13Assignment.Models
{
    //This class provides the template for all instances of bowlers
    public class Bowler
    {
        //Sets the primary key
        [Key]
        [Required]
        public int BowlerID { get; set; }
        public string BowlerLastName { get; set; }
        public string BowlerFirstName { get; set; }

        //Sets max length of middle initial
        [MaxLength(1)]
        public string BowlerMiddleInit { get; set; }
        public string BowlerAddress { get; set; }
        public string BowlerCity { get; set; }

        //Sets max length for the states
        [MaxLength(2)]
        public string BowlerState { get; set; }
        public string BowlerZip { get; set; }
        public string BowlerPhoneNumber { get; set; }
        
        //Foreign Key Relationship
        public int TeamID { get; set; }

        public Team Team { get; set; }
    }
}
