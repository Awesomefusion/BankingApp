namespace Domain
{
    using System;
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AccountNumber { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 50;
    }
}