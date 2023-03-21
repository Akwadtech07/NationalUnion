using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NationalUnion.Models;
using NationalUnion.Models.Entity;

namespace NationalUnion.ApplicationContext
{
    public class ApplicatioDbContext : DbContext
    {
        public ApplicatioDbContext(DbContextOptions<ApplicatioDbContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins  { get; set; }
        public DbSet<Driver> Drivers  { get; set; }
        public DbSet<Ticket> Tickets  { get; set; }
        public DbSet<Bus> Buses  { get; set; }
        public DbSet<User> Users  { get; set; }
        public DbSet<Role> Roles  { get; set; }
        public DbSet<UserRole> userRoles  { get; set; }

    }
}