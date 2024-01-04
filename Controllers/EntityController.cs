using System.Diagnostics;
using LAB12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB12.Controllers;

public class EntityController : Controller
{
    private readonly UserContext db;

    public EntityController(UserContext context)
    {
        db = context;
        InitializeDatabaseAsync().Wait();
    }

    public async Task InitializeDatabaseAsync()
    {
        if (!await db.Users.AnyAsync())
        {
            db.Users.AddRange(
                new User { FirstName = "Dread", LastName = "Lord", Age = 19 },
                new User { FirstName = "Kirill", LastName = "Shkolniy", Age = 19 },
                new User { FirstName = "Mister", LastName = "Zelensky", Age = 35 }
            );

            await db.SaveChangesAsync();
        }

        if (!await db.Companies.AnyAsync())
        {
            db.Companies.AddRange(
                new Company { Name = "Coca-Cola", Year = 1892 },
                new Company { Name = "Ford", Year = 1903 },
                new Company { Name = "McDonald's", Year = 1940 },
                new Company { Name = "Nike", Year = 1964 },
                new Company { Name = "Visa", Year = 1958 },
                new Company { Name = "Disney", Year = 1923 },
                new Company { Name = "Pfizer", Year = 1849 },
                new Company { Name = "Unilever", Year = 1929 },
                new Company { Name = "Starbucks", Year = 1971 },
                new Company { Name = "Adidas", Year = 1949 }

            );

            await db.SaveChangesAsync();
        }
    }

    public async Task<IActionResult> Index()
    {
        var users = await db.Users.ToListAsync();
        foreach (var user in users)
        {
            Console.WriteLine($"User: {user.FirstName} {user.LastName}, Age: {user.Age}");
            Debug.WriteLine($"User: {user.FirstName} {user.LastName}, Age: {user.Age}");
        }

        var companies = await db.Companies.ToListAsync();

        var viewModel = new HomeIndexViewModel
        {
            Users = users,
            Companies = companies
        };

        return View(viewModel);
    }
}

public class HomeIndexViewModel
{
    public List<User> Users { get; set; }
    public List<Company> Companies { get; set; }
}