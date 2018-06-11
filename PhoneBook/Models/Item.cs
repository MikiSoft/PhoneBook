using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Item
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        [Required]
        [RegularExpression(@"^[\p{L} .&'-]+$", ErrorMessage = "Invalid First Name.")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Required]
        [RegularExpression(@"^[\p{L} .&'-]+$", ErrorMessage = "Invalid Last Name.")]
        public string LastName { get; set; }

        [StringLength(15)]
        [Index(IsUnique = true)]
        [Required]
        [RegularExpression(@"^[0-9\-\+]{9,15}$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNum { get; set; }
    }
}