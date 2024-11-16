using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("memberships")]
public partial class Membership
{
    [Key]
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public int SupscriptionId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? OrderCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    public int StatusId { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("Memberships")]
    public virtual User Owner { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Memberships")]
    public virtual MembershipStatus Status { get; set; } = null!;

    [ForeignKey("SupscriptionId")]
    [InverseProperty("Memberships")]
    public virtual Subcription Supscription { get; set; } = null!;
}
