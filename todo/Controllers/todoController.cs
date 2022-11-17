using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todo.database;
using todo.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace todo.Controllers;

public class todoController : Controller
{
    private readonly todocontext context;

    public todoController(todocontext context)
    {
        this.context = context; 
    }
    public async Task<ActionResult> Index()
    {
        IQueryable<todolist> items = from i in context.todo orderby i.Id select i;

        List<todolist> todolist = await items.ToListAsync(item);

        return View(todolist);
    }

    public IActionResult Create() => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(todolist item)
    {
        if (ModelState.IsValid)
        {
            context.Add(item);
            await context.SaveChangesAsync();
            TempData["Success"] = "New Activity added";
            return RedirectToAction("Index");
        }

        return View(item);
    }

    public async Task<ActionResult> Edit (int id)
    {
        todolist item = await context.todo.FindAsync(id);
        if(item == null)
        {
            return NotFound();
        }
        return View(item);
    }

     
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(todolist item)
    {
        if (ModelState.IsValid)
        {
            context.Update(item);
            await context.SaveChangesAsync();
            TempData["Success"] = "Activity Updated";
            return RedirectToAction("Index");
        }

        return View(item);
    }


    public async Task<ActionResult> Delete(int id)
    {
        todolist item = await context.todo.FindAsync(id);
        if (item == null)
        {
            TempData["Success"] = "Activity does not exist";
 
        }
        else
        {
            context.todo.Remove(item);
            await context.SaveChangesAsync();
            TempData["Success"] = "Activity deleted";

        }
        return RedirectToAction("Index");
    }
}

