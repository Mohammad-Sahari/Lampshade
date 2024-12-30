namespace _01_Framework.Domain
{
    public class DomainBase
    {
        public long Id { get; private set; }
        public DateTime CreationDate { get; private set; }

        public DomainBase()
        {
            CreationDate = DateTime.Now;
        }
    }
}
