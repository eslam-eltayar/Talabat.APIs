﻿using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }

        [Required]
        public string BasketId { get; set; }

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public OrderAddressDto ShippingAddress { get; set; }
    }
}
