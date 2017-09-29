using System;

namespace Models
{
    public class Debt
    {
        public int DebtId { get; set; }
        public int PersonId { get; set; }
        public decimal Amout { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DueDate { get; set; }
        public bool DebtStatus { get; set; }
        public decimal PaidAmout { get; set; }
    }
}
