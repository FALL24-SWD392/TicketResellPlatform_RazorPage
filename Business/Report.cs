using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("reports")]
public partial class Report
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int SenderId { get; set; }

    [StringLength(255)]
    public string Message { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Evidence { get; set; } = null!;

    public int StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Reports")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("Reports")]
    public virtual User Sender { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Reports")]
    public virtual ReportStatus Status { get; set; } = null!;
}
