using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KrushiSevaKendra.Models;

[Table("recommendations")]
public partial class Recommendation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("rdate", TypeName = "date")]
    public DateTime? Rdate { get; set; }

    [Column("cropid")]
    public int? Cropid { get; set; }

    [Column("farmerid")]
    public int? Farmerid { get; set; }

    [Column("message")]
    public string Message { get; set; }

    [ForeignKey("Cropid")]
    [InverseProperty("Recommendations")]
    public virtual Crop Crop { get; set; } = null!;

    [ForeignKey("Farmerid")]
    [InverseProperty("Recommendations")]
    public virtual Farmer Farmer { get; set; } = null!;
}
