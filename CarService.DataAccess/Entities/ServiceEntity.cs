using CarService.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Entities;

[Table("services")]
public class ServiceEntity : BaseEntity
{
    public string Name { get; set; }
    public int Price{ get; set; }

    public virtual ICollection<OrderEntity> Orders { get; set; }
}