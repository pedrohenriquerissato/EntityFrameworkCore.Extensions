using Bogus;
using EntityFrameworkCore.Extensions.Persistence;
using Xunit;

namespace EntityFrameworkCore.Extensions.Tests
{
    public class ContextExtensionsTests
    {
        [Fact]
        public void TextExtension()
        {
            var employee = new Faker<Employee>()
                .RuleFor(x => x.Dob, r => r.Person.DateOfBirth.Date)
                .RuleFor(x => x.FirstName, r => r.Person.FirstName)
                .RuleFor(x => x.LastName, r => r.Person.LastName);

            var employees = employee.Generate(10);

            using (var db = new dbtestContext())
            {
                using (var entity = db.Database.BeginTransaction())
                {
                    db.BulkMerge(employees);
                    entity.Commit();
                }
            };

            Assert.True(true);
        }
    }
}
