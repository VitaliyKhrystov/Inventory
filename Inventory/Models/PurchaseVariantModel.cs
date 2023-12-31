﻿using Inventory.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class PurchaseVariantModel
    {
        public Guid Id { get; set; }
        [Required]
        public int SerialNumber { get; set; }
        public Guid ProductEntityId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public Guid? VariantEntytiId { get; set; }
        public string ProductVariantId { get; set; }

        public int? Quantity { get; set; } = default(int);
        public decimal? ProductRate { get; set; } = default(decimal);

        public decimal? Amount { get; set; } = default(decimal);

        public int? Discount { get; set; } = default(int);
        public decimal? AmountAfterDiscount { get; set; } = default(decimal);
        public ProductEntity Product { get; set; }
    }
}
