using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ParserDAL.Models;

public class PersonalChange
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
    public int pair_number { get; set; }

    [Required]
    public string changes { get; set; }
}
