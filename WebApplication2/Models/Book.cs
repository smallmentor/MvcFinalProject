using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Writer
    {
        public string WriterID { get; set; }
        [DisplayName("作者")]
        public string WiterName { get; set; }


        public virtual ICollection<WriterBook> WriterBooks { get; set; }
    }

    public class WriterBook
    {
        public int ID { get; set; }
        [DisplayName("作者ID")]
        public int WriterID { get; set; }
        [DisplayName("書本ID")]
        public int BookID { get; set; }


        public virtual Writer Writer { get; set; }

        public virtual Book Book { get; set; }
    }


    public class Book
    {
        public int BookID { get; set; }
        [DisplayName("書名")]
        public string BookName { get; set; }
        [DisplayName("價格")]
        public int Price { get; set; }
        [DisplayName("ISBN")]
        public string ISBN { get; set; }
        [DisplayName("出版社ID")]
        public int PubID { get; set; }


        public virtual ICollection<WriterBook> WriterBook { get; set; }

        public virtual Publisher Publisher { get; set; }
    }


    public class Publisher
    {
        [Key]
        public int PubID { get; set; }
        [DisplayName("出版社")]
        public string PubName { get; set; }


        public virtual ICollection<Book> Books { get; set; }
    }

    public class BookDBConnection : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Writer> Writers { get; set; }

        public DbSet<WriterBook> WriterBooks { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
    }
}