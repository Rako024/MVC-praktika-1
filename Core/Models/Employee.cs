using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

public class Employee
{
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Position { get; set; }
    public string? ImgUrl { get; set; }
    [NotMapped]
    public IFormFile? PhotoFile { get; set; }
}
