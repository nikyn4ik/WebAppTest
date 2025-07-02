using Microsoft.AspNetCore.Mvc;
using WebAppTest.Domain;
using WebAppTest.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAppTest.Controllers;

public class OrdersController : Controller
{
    private readonly AppDbContext _context;
    
    public OrdersController(AppDbContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        try
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }
        catch (Exception ex)
        {
            return Problem("Ошибка при загрузке списка заказов: " + ex.Message);
        }
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Orders order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }
        try
        {
            _context.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException dbEx)
        {
            ModelState.AddModelError("", "Ошибка при сохранении заказа: " + dbEx.Message);
            return View(order);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Произошла ошибка: " + ex.Message);
            return View(order);
        }
    }
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return NotFound();

            return View(order);
        }
        catch (Exception ex)
        {
            return Problem("Ошибка при загрузке данных заказа: " + ex.Message);
        }
    }
}