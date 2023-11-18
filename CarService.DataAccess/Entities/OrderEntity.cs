using CarService.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Entities;

[Table("orders")]
public class OrderEntity : BaseEntity
{
    public string Status { get; set; }
    public DateTime Time { get; set; }
    public string Comment{ get; set; }

    public int CarId { get; set; }
    public CarEntity Car { get; set; }

    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; }

    public virtual ICollection<FeedbackEntity> Feedbacks { get; set; }
}