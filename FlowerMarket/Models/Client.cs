using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FlowerMarket.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [RegularExpression(".*@.*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Range(18, 100)]
        public int Age { get; set; }
        [Required]
        [Range(1, 25)]
        public int WorkExperience { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\+\d{3}\-\d{2}\-\d{3}\-\d{2}\-\d{2}", ErrorMessage = "Please type as +998-XX-YYY-ZZ-NN")]
        public string Phone { get; set; }
    }
}