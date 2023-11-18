using CarService.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Entities;

[Table("brands")]
public class BrandEntity : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<CarEntity> Cars { get; set; }
}