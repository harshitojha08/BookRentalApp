using System;
using System.Collections.Generic;



namespace DAL.DataEntities
{
    public partial class User
    {
        public User()
        {
            BookBorrowerUsers = new HashSet<Book>();
            BookLenderUsers = new HashSet<Book>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int TokensAvailable { get; set; }

        public virtual ICollection<Book> BookBorrowerUsers { get; set; }
        public virtual ICollection<Book> BookLenderUsers { get; set; }
    }
}
