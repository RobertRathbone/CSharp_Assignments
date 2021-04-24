using Microsoft.EntityFrameworkCore;

namespace ChefsandDishes2.Models
{
    public class ChefsandDishes2Context : DbContext
    {
        public ChefsandDishes2Context(DbContextOptions options) : base(options) { }
 
        // for every model / entity that is going to be part of the db
        // the names of these properties will be the names of the tables in the db
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Chef> Chefs { get; set; }
 
    }
}