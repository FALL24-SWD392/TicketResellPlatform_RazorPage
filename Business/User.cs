using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("users")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(30)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Avatar { get; set; }

    public double? Rating { get; set; }

    public int? Reputation { get; set; }

    public int RoleId { get; set; }

    public int StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [InverseProperty("Sender")]
    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    [InverseProperty("Buyer")]
    public virtual ICollection<Chatbox> ChatboxBuyers { get; set; } = new List<Chatbox>();

    [InverseProperty("Seller")]
    public virtual ICollection<Chatbox> ChatboxSellers { get; set; } = new List<Chatbox>();

    [InverseProperty("Owner")]
    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    [InverseProperty("Sender")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Users")]
    public virtual UserStatus Status { get; set; } = null!;

    [InverseProperty("Owner")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
