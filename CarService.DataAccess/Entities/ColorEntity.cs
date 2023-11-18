using CarService.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Entities;

[Table("colors")]
public class ColorEntity : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<CarEntity> Cars { get; set; }
}