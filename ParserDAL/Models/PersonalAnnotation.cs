using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParserDAL.Models;

[Table("personal_annotations")]
public class PersonalAnnotation
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
    public string annotation { get; set; }
}
