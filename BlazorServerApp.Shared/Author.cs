using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorServerApp.Shared
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
