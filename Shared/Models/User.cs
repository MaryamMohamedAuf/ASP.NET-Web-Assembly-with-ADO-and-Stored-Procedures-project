using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SOFCOproject.Shared.Models
{
    public class User
    { 
            public int Id { get; set; }

            [Required(ErrorMessage = "First Name is required")]
            public string Name { get; set; }

            [Required(ErrorMessage = " Email Address is required")]
            public string Email { get; set; }

        }
    }

