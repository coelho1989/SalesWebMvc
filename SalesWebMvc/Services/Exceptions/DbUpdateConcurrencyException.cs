namespace SalesWebMvc.Services.Exceptions
{
    public class DbUpdateConcurrencyException : ApplicationException
    {
        public DbUpdateConcurrencyException(string message) : base(message)
        {
        }
    }
}
