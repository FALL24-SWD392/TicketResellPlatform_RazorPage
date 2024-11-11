using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("feedbacks")]
public partial class Feedback
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    [StringLength(255)]
    public string Message { get; set; } = null!;

    public int Rating { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Feedbacks")]
    public virtual Order Order { get; set; } = null!;
}
