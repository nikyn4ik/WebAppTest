using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models;

public class Orders
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // уникальный ид заказа, генерируется бд

    [Required]
    [Display(Name = "Город отправителя")]
    public string SenderCity { get; set; } // город отправителя

    [Required]
    [Display(Name = "Адрес отправителя")]
    public string SenderAddress { get; set; } // адрес отправителя

    [Required]
    [Display(Name = "Город получателя")]
    public string RecipientCity { get; set; } // город получателя

    [Required]
    [Display(Name = "Адрес получателя")]
    public string RecipientAddress { get; set; } // адрес получателя

    [Required]
    [Range(0.1, 10000)]
    [Display(Name = "Вес груза (кг)")]
    public double CargoWeight { get; set; } // вес груза в кг

    [Required]
    [Display(Name = "Дата забора груза")]
    public DateTime PickupDate { get; set; } // дата забора груза
}