using CarService.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Entities;

[Table("users")]
public class UserEntity : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Status { get; set; }

    public virtual ICollection<CarEntity> Cars { get; set; }
}