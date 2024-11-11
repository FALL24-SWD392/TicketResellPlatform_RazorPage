using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("ticket_types")]
public partial class TicketType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Type { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
