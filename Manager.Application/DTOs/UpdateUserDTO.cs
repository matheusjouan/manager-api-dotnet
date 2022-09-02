using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application.DTOs;

public class UpdateUserDTO
{
    [Required]
    public long Id { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Password { get; set; }
}

