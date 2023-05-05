using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KrushiSevaKendra.Models;

[Table("towns")]
public partial class Town
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [InverseProperty("Town")]
    public virtual ICollection<Farmer> Farmers { get; } = new List<Farmer>();
}
