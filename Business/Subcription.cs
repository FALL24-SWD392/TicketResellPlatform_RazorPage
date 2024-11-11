using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Business;

[Table("subcriptions")]
public partial class Subcription
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string Description { get; set; } = null!;

    public int SaleLimit { get; set; }

    public int Price { get; set; }

    [InverseProperty("Supscription")]
    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}
