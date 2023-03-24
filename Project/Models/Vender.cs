using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Vender
    {
        [Key]
        public int Id { get; set; }
       
        public string Organization { get; set; }
     
        public string Vender_Type { get; set; }
    
        public string City { get; set; }
      
        public string Country { get; set; }
        
        public string Phone { get; set; }
       
        public DateTime Founded { get; set; }
    }
}