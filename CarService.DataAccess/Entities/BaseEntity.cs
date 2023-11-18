using System.ComponentModel.DataAnnotations;

namespace CarService.DataAccess.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public Guid ExternalId { get; set; } // unique index - unique optional
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
}