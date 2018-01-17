using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class SUser
    {
        [Key]
        public int Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

    }

    public class SUserRole
    {
        public int Id { get; set; }

        public string Account { get; set; }

        public string RoleId { get; set; }
    }


    public class SRole
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }


    }

    public class DB2 : DbContext
    {
        public DbSet<SUser> SUsers { get; set; }
        public DbSet<SRole> SRoles { get; set; }
        public DbSet<SUserRole> SUserRoles { get; set; }
    }
}