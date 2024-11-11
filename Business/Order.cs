using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("orders")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    public int ChatBoxId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [ForeignKey("ChatBoxId")]
    [InverseProperty("Orders")]
    public virtual Chatbox ChatBox { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [InverseProperty("Order")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
