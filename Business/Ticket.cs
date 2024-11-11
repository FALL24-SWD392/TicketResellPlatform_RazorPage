using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("tickets")]
public partial class Ticket
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Image { get; set; } = null!;

    public int OwnerId { get; set; }

    public int TypeId { get; set; }

    public int StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpiryDate { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [InverseProperty("Ticket")]
    public virtual ICollection<Chatbox> Chatboxes { get; set; } = new List<Chatbox>();

    [ForeignKey("OwnerId")]
    [InverseProperty("Tickets")]
    public virtual User Owner { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Tickets")]
    public virtual TicketStatus Status { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("Tickets")]
    public virtual TicketType Type { get; set; } = null!;
}
