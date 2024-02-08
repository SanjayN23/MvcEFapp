using Microsoft.EntityFrameworkCore;

namespace MvcEFApp.Models
{
    public class RepositoryAppointment
    {
        public static List<Appointment> GetAppointment()
        {
            HospitalDbContext ctx = new HospitalDbContext();
            var list = ctx.Appointments.ToList();
            return list;
        }
        public static Appointment GetAppointmentById(int id)
        {
            HospitalDbContext ctx = new HospitalDbContext();
            var appointment = ctx.Appointments.Find(id);
            return appointment;
        }
        public static void AddNewAppointment(Appointment appointment)
        {
            HospitalDbContext ctx = new HospitalDbContext();
            ctx.Appointments.Add(appointment);
            ctx.SaveChanges();
        }
        public static void ModifyAppointment(Appointment appointment)
        {
            HospitalDbContext ctx = new HospitalDbContext();
            ctx.Entry(appointment).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        public static void RemoveAppointment(int id)
        {
            HospitalDbContext ctx = new HospitalDbContext();
            Appointment appointment = ctx.Appointments.Find(id);
            ctx.Appointments.Remove(appointment);
            ctx.SaveChanges();
        }
    }
}
