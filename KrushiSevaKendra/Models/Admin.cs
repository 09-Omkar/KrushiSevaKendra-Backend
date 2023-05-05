using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KrushiSevaKendra.Models;

[Table("admins")]
public partial class Admin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("username")]
    [StringLength(100)]
    [Unicode(false)]
    public string Username { get; set; }

    [Column("password")]
    [StringLength(100)]
    [Unicode(false)]
    public string Password { get; set; }
    
    [NotMapped]
    public string TokenString { get; set; }
}
