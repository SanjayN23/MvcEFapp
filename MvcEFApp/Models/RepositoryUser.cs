using Microsoft.EntityFrameworkCore;
using MvcEFApp.Models;
using System.Collections.Generic;
using System.Linq;

public class RepositoryUser
{
    public static List<User> GetUsers()
    {
        using (HospitalDbContext ctx = new HospitalDbContext())
        {
            return ctx.Users.ToList();
        }
    }

    public static User GetUserById(int id)
    {
        using (HospitalDbContext ctx = new HospitalDbContext())
        {
            return ctx.Users.Find(id);
        }
    }

    public static void AddNewUser(User user)
    {
        using (HospitalDbContext ctx = new HospitalDbContext())
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }
    }

    public static void ModifyUser(User user)
    {
        using (HospitalDbContext ctx = new HospitalDbContext())
        {
            ctx.Entry(user).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }

    public static void RemoveUser(int id)
    {
        using (HospitalDbContext ctx = new HospitalDbContext())
        {
            User user = ctx.Users.Find(id);
            if (user != null)
            {
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            }
        }
    }

    public static User AuthenticateUser(string email, string password)
    {
        using (HospitalDbContext ctx = new HospitalDbContext())
        {
            // For simplicity, assuming you have a table named Users in your database
            // and you are checking email and password against this table
            return ctx.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }

    public static async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        using (HospitalDbContext ctx = new HospitalDbContext())
        {
            // For simplicity, assuming you have a table named Users in your database
            // and you are checking email and password against this table
            return await ctx.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}