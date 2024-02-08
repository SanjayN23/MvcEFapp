// Controllers\UserController.cs
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcEFApp.Models;
using System.Security.Claims;

public class UserController : Controller
{
    public IActionResult Index()
    {
        List<User> users = RepositoryUser.GetUsers();
        return View(users);
    }

    public IActionResult Details(int id)
    {
        User user = RepositoryUser.GetUserById(id);
        return View(user);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                RepositoryUser.AddNewUser(user);
                TempData["SuccessMessage"] = "User successfully registered!";
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            TempData["ErrorMessage"] = $"Error registering user: {ex.Message}";
        }

        return View();
    }


    public IActionResult Edit(int id)
    {
        User user = RepositoryUser.GetUserById(id);
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, User user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                RepositoryUser.ModifyUser(user);
            }
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public IActionResult Delete(int id)
    {
        User user = RepositoryUser.GetUserById(id);
        return View(user);
    }

    /* public IActionResult Login()
     {
         return View();
     }

     [HttpPost]
     //[ValidateAntiForgeryToken]
     public IActionResult Login(LoginViewModel loginModel)
     {
         if (ModelState.IsValid)
         {
             // Check the credentials against the database
             User user = RepositoryUser.AuthenticateUser(loginModel.Email, loginModel.Password);

             if (user != null)
             {
                 // Authentication successful, you may want to store user details in a session or cookie
                 TempData["SuccessMessage"] = "Login successful!";
                 return RedirectToAction("Index", "Home"); // Redirect to the home page or any other page
             }
             TempData["ErrorMessage"] = "Invalid login attempt.";
             //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
         }

         return View(loginModel);
     }*/


    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel modelLogin)
    {
        var user = await RepositoryUser.GetUserByEmailAndPasswordAsync(modelLogin.Email, modelLogin.Password);

        if (user != null)
        {
            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim("OtherProperties", "Example Role")
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = modelLogin.KeepLoggedIn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        ViewData["ValidateMessage"] = "User not found";
        return View();
    }




}
