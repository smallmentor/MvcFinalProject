using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("帳號")]
        public string Account { get; set; }
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("密碼")]
        public string Password { get; set; }

    }

    public class SUserRole
    {
        public int Id { get; set; }
        [DisplayName("帳號")]
        public string Account { get; set; }
        [DisplayName("群組")]
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