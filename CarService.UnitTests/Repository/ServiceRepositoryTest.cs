using CarService.DataAccess.Entities;
using CarService.DataAccess;
using FluentAssertions;
using NUnit.Framework;

namespace CarService.UnitTests.Repository;

[TestFixture]
[Category("Integration")]
public class ServiceRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllServicesTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var services = new ServiceEntity[]
        {
            new ServiceEntity()
            {
                Name = "testName1",
                Price = 5000,
                ExternalId = Guid.NewGuid()
            },
             new ServiceEntity()
             {
                Name = "testName2",
                Price = 10000,
                ExternalId = Guid.NewGuid()
             }
        };
        context.Services.AddRange(services);
        context.SaveChanges();

        //execute
        var repository = new Repository<ServiceEntity>(DbContextFactory);
        var actualServices = repository.GetAll();

        //assert
        actualServices.Should().BeEquivalentTo(services, options => options.Excluding(x => x.Orders));
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var services = new ServiceEntity[]
        {
            new ServiceEntity()
            {
                Name = "testName1",
                Price = 5000,
                ExternalId = Guid.NewGuid()
            },
             new ServiceEntity()
             {
                Name = "testName2",
                Price = 10000,
                ExternalId = Guid.NewGuid()
             }
        };
        context.Services.AddRange(services);
        context.SaveChanges();

        //execute
        var repository = new Repository<ServiceEntity>(DbContextFactory);
        var actualServices = repository.GetAll(x => x.Name == "testName1").ToArray();

        //assert
        actualServices.Should().BeEquivalentTo(services.Where(x => x.Name == "testName1"),
            options => options.Excluding(x => x.Orders));
    }

    [Test]
    public void SaveNewUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var service = new ServiceEntity()
        {
            Name = "testName1",
            Price = 5000,
            ExternalId = Guid.NewGuid()
        };
        var repository = new Repository<ServiceEntity>(DbContextFactory);
        repository.Save(service);

        //assert
        var actualService = context.Services.SingleOrDefault();
        actualService.Should().BeEquivalentTo(service, options => options.Excluding(x => x.Orders)
            .Excluding(x => x.Id)
            .Excluding(x => x.ModificationTime)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ExternalId));

        actualService.Id.Should().NotBe(default);
        actualService.ModificationTime.Should().NotBe(default);
        actualService.CreationTime.Should().NotBe(default);
        actualService.ExternalId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void UpdateUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var service = new ServiceEntity()
        {
            Name = "testName1",
            Price = 5000,
            ExternalId = Guid.NewGuid()
        };
        context.Services.Add(service);
        context.SaveChanges();

        //execute

        service.Name = "newName";
        service.Price = 2999;
        var repository = new Repository<ServiceEntity>(DbContextFactory);
        repository.Save(service);

        //assert
        var actualService = context.Services.SingleOrDefault();
        actualService.Should().BeEquivalentTo(service, options => options.Excluding(x => x.Orders));
    }

    [Test]
    public void DeleteUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var service = new ServiceEntity()
        {
            Name = "testName1",
            Price = 5000,
            ExternalId = Guid.NewGuid()
        };
        context.Services.Add(service);
        context.SaveChanges();

        //execute
        var repository = new Repository<ServiceEntity>(DbContextFactory);
        repository.Delete(service);

        //assert
        context.Services.Count().Should().Be(0);
    }

    [SetUp]
    public void SetUp()
    {
        CleanUp();
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();
    }

    public void CleanUp()
    {
        using (var context = DbContextFactory.CreateDbContext())
        {
            context.Services.RemoveRange(context.Services);
            context.Orders.RemoveRange(context.Orders);
            context.SaveChanges();
        }
    }
}