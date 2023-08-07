namespace Mc2.CrudTest.Core.Domain
{
    public class CustomerAggregate
    {
        private Guid _id;
        public CustomerAggregate()
        {
            _id = Guid.NewGuid();
        }
        public Guid GetId() => _id;
    }
}