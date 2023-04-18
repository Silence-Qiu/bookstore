using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; } = default!;

        public DbSet<Student> Students { get; set; } = default!;

        public DbSet<Book> Books { get; set; } = default!;

        public DbSet<StudentBorrowBooks> StudentBorrowBooks { get; set; } = default!;

       
    }
}
