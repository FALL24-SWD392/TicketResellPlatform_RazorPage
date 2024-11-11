using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("chat_messages")]
public partial class ChatMessage
{
    [Key]
    public int Id { get; set; }

    public int ChatBoxId { get; set; }

    public int SenderId { get; set; }

    [StringLength(255)]
    public string Message { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [ForeignKey("ChatBoxId")]
    [InverseProperty("ChatMessages")]
    public virtual Chatbox ChatBox { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("ChatMessages")]
    public virtual User Sender { get; set; } = null!;
}
