namespace Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 50;
    }
}