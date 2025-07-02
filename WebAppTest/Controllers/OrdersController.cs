using Microsoft.AspNetCore.Mvc;
using WebAppTest.Domain;
using WebAppTest.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAppTest.Controllers;

public class OrdersController : Controller
{
    private readonly AppDbContext _context; // контекст бд

    public OrdersController(AppDbContext context)
    {
        _context = context; // инициализация контекста через инъекцию
    }

    // get список всех заказов
    public IActionResult Index()
    {
        try
        {
            var orders = _context.Orders.ToList(); // получение списка заказов
            return View(orders); // передать в представление
        }
        catch (Exception ex)
        {
            return Problem("Ошибка при загрузке списка заказов: " + ex.Message); // ошибка при загрузке
        }
    }

    // get форма создания заказа
    public IActionResult Create()
    {
        return View(); // показать форму создания
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // post создание нового заказа
    public async Task<IActionResult> Create(Orders order)
    {
        if (!ModelState.IsValid)
        {
            return View(order); // если модель не валидна, вернуть форму с ошибками
        }
        try
        {
            _context.Add(order); // добавить заказ в контекст
            await _context.SaveChangesAsync(); // сохранить изменения в бд

            return RedirectToAction(nameof(Index)); // перейти к списку заказов
        }
        catch (DbUpdateException dbEx)
        {
            ModelState.AddModelError("", "Ошибка при сохранении заказа: " + dbEx.Message); // ошибка обновления базы
            return View(order);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Произошла ошибка: " + ex.Message); // общая ошибка
            return View(order);
        }
    }

    // get детали заказа по id
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id); // поиск заказа по id
            if (order == null)
                return NotFound(); // если не найден - 404

            return View(order); // вернуть детали заказа
        }
        catch (Exception ex)
        {
            return Problem("Ошибка при загрузке данных заказа: " + ex.Message); // ошибка при загрузке деталей
        }
    }
}
