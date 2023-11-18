using CarService.DataAccess.Entities;
using CarService.DataAccess;
using FluentAssertions;
using NUnit.Framework;

namespace CarService.UnitTests.Repository;

[TestFixture]
[Category("Integration")]
public class UserRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllUsersTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
  
        var users = new UserEntity[]
        {
            new UserEntity()
            {
                Name = "testName1",
                Email = "testEmail1",
                PasswordHash = "passwordTest",
                Status = "testStatus1",
                ExternalId = Guid.NewGuid()
            },
             new UserEntity()
             {
                Name = "testName2",
                Email = "testEmail2",
                PasswordHash = "passwordTest2",
                Status = "testStatus2",
                ExternalId = Guid.NewGuid()
             }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll();

        //assert
        actualUsers.Should().BeEquivalentTo(users, options => options.Excluding(x => x.Cars));
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var users = new UserEntity[]
         {
            new UserEntity()
            {
                Name = "testName1",
                Email = "testEmail1",
                PasswordHash = "passwordTest1",
                Status = "testStatus1",
                ExternalId = Guid.NewGuid()
            },
             new UserEntity()
            {
                Name = "testName2",
                Email = "testEmail2",
                PasswordHash = "passwordTest2",
                Status = "testStatus2",
                ExternalId = Guid.NewGuid()
            }
         };
        context.Users.AddRange(users);
        context.SaveChanges();
        
        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll(x => x.Name == "testName1").ToArray();

        //assert
        actualUsers.Should().BeEquivalentTo(users.Where(x => x.Name == "testName1"),
            options => options.Excluding(x => x.Cars));
    }

    [Test]
    public void SaveNewUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        //execute
        var user = new UserEntity()
        {
            Name = "testName1",
            Email = "testEmail1",
            PasswordHash = "passwordTest1",
            Status = "testStatus1",
            ExternalId = Guid.NewGuid()
        };
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.Cars)
            .Excluding(x => x.Id)
            .Excluding(x => x.ModificationTime)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ExternalId));

        actualUser.Id.Should().NotBe(default);
        actualUser.ModificationTime.Should().NotBe(default);
        actualUser.CreationTime.Should().NotBe(default);
        actualUser.ExternalId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void UpdateUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var user = new UserEntity()
        {
            Name = "testName",
            Email = "testEmail",
            PasswordHash = "passwordTest",
            Status = "testStatus",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        //execute

        user.Name = "newName";
        user.Email = "newEmail";
        user.PasswordHash = "newPassword";
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.Cars));
    }

    [Test]
    public void DeleteUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();

        var user = new UserEntity()
        {
            Name = "testName2",
            Email = "testEmail2",
            PasswordHash = "passwordTest2",
            Status = "testStatus2",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(user);
        context.SaveChanges();

        //execute
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Delete(user);

        //assert
        context.Users.Count().Should().Be(0);
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
            context.Users.RemoveRange(context.Users);
            context.Cars.RemoveRange(context.Cars);
            context.SaveChanges();
        }
    }
}