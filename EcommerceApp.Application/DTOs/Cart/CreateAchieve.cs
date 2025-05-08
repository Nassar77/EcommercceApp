﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Application.DTOs.Cart;
public class CreateAchieve
{
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public Guid UserId { get; set; }
}
