using System.ComponentModel.DataAnnotations;

namespace ParserDAL.Models;

public class PersonalSchedule
{
    [Required]
    public int id { get; set; }

    [Required]
    [MaxLength(20)]
    public string stream_group { get; set; }

    [Required]
    [MaxLength(20)]
    public string day { get; set; }

    [Required]
    [MaxLength(20)]
    public string parity { get; set; }

    [Required]
    public string schedule { get; set; }
}