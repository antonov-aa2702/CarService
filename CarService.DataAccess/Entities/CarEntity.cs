using CarService.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Entities;

[Table("cars")]
public class CarEntity : BaseEntity
{
    public int Year { get; set; }
    public string Number { get; set; }

    public int UserId {  get; set; }
    public UserEntity User { get; set; }

    public int ColorId { get; set; }
    public ColorEntity Color { get; set; }

    public int BrandId { get; set; }
    public BrandEntity Brand { get; set; }

    public virtual ICollection<OrderEntity> Orders { get; set; }
}