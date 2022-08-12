using System.ComponentModel.DataAnnotations;

namespace ParserDAL.Models;

public class HeadmanAnnotation
{
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
