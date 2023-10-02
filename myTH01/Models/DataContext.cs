﻿using Microsoft.EntityFrameworkCore;

namespace myTH01.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        }

        public DbSet<Menu> Menus { get; set; }
    }
}