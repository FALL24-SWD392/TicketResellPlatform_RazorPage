using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("chatboxes")]
public partial class Chatbox
{
    [Key]
    public int Id { get; set; }

    public int BuyerId { get; set; }

    public int SellerId { get; set; }

    public int TicketId { get; set; }

    public int StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [ForeignKey("BuyerId")]
    [InverseProperty("ChatboxBuyers")]
    public virtual User Buyer { get; set; } = null!;

    [InverseProperty("ChatBox")]
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    [InverseProperty("ChatBox")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("SellerId")]
    [InverseProperty("ChatboxSellers")]
    public virtual User Seller { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Chatboxes")]
    public virtual ChatboxStatus Status { get; set; } = null!;

    [ForeignKey("TicketId")]
    [InverseProperty("Chatboxes")]
    public virtual Ticket Ticket { get; set; } = null!;
}
