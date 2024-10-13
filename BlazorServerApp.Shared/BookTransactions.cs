using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorServerApp.Shared
{
    public class BookTransaction
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string TransactionType { get; set; } = "LOAN"; // 'Loan' or 'Sale'
        public int Quantity { get; set; } 
        public string LoanerId { get; set; }
        public string LoanerDetails { get; set; } // JSON field for loaner details
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public DateTime ValidUntil { get; set; } = DateTime.Now.AddDays(30);
    }

}
