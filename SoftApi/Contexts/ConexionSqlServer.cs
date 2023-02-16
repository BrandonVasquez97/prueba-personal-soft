using Microsoft.EntityFrameworkCore;
using SoftApi.Models;

namespace SoftApi.Contexts
{
    public class ConexionSqlServer:DbContext
    {
        public ConexionSqlServer(DbContextOptions<ConexionSqlServer> options): base(options){

        }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}