﻿using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
    }
}
