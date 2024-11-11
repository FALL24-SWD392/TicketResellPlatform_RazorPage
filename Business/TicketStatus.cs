using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("ticket_status")]
public partial class TicketStatus
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
