using CarService.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Entities;

[Table("feedbacks")]
public class FeedbackEntity : BaseEntity
{
    public DateTime Time { get; set; }
    public string Text { get; set; }

    public int OrderId { get; set; }
    public OrderEntity Order { get; set; }
}