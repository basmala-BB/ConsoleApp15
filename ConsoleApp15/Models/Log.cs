using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp15.Models;

[Table("logs")]
public partial class Log
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("message")]
    [StringLength(255)]
    public string? Message { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
