using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KrushiSevaKendra.Models;

[Table("crops")]
public partial class Crop
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; }

    [InverseProperty("Crop")]
    public virtual ICollection<Recommendation> Recommendations { get; } = new List<Recommendation>();
}
