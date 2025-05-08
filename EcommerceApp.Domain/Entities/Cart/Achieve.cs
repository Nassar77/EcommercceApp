using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Domain.Entities.Cart;
public class Achieve
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedData { get; set; } = DateTime.Now;

}
