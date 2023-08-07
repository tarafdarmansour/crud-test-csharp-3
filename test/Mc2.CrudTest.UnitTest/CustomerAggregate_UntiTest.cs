using System.ComponentModel.DataAnnotations;
using Mc2.CrudTest.Core.Domain;
using Shouldly;

namespace Mc2.CrudTest.UnitTest
{
    public class CustomerAggregate_UntiTest
    {
        [Fact]
        public void WhenICreateNewCustomerAggregate_ItShouldHaveValidId()
        {
            var customer = new CustomerAggregate();
            Guid.TryParse(customer.GetId().ToString(), out Guid _).ShouldBe(true);
            
        }
    }
}