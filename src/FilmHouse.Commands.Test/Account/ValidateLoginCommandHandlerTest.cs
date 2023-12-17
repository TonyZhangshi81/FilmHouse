using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data;
using FilmHouse.Data.Entities;
using Xunit;

namespace FilmHouse.Commands.Test.Account;

public class ValidateLoginCommandHandlerTest
{
    private readonly FilmHouseDbContext dbContext;

    public ValidateLoginCommandHandlerTest()
    {
        this.dbContext = new InMemoryDatabaseFactory().CreateContext();

        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);
        var userAccount = new UserAccountEntity();
        userAccount.RequestId = uuid;
        userAccount.UserId = new(Guid.NewGuid());
        userAccount.Account = new("tonyzhangshi");
        userAccount.PasswordHash = new(new PasswordHashVO("Tony19811031").ToHash("tonyzhangshi"));
        userAccount.EmailAddress = new("tonyzhangshi@163.com");
        userAccount.Avatar = new("0ACFC82E7D5A41FC8AB8FD4EF603C858Tony.jpg");
        userAccount.Cover = new("Cover_1.jpg");
        userAccount.IsAdmin = new(false);
        userAccount.LastLoginIp = new("201.182.1.23");
        userAccount.CreatedOn = sysDate;

        //this.dbContext.UserAccounts.Add(userAccount);
        //this.dbContext.SaveChanges();

        //Assert.Equal(3, this.dbContext.UserAccounts.Count());
    }

    [Fact]
    public void MyTest()
    {
        //Assert.Equal(3, this.dbContext.UserAccounts.Count());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private IEnumerable<UserAccountEntity> GetUserAccounts(RequestIdVO uuid, CreatedOnVO dateTime)
    {
        return new List<UserAccountEntity>
       {
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new AccountNameVO("tonyzhangshi"), PasswordHash = new PasswordHashVO(new PasswordHashVO("Tony19811031").ToHash("tonyzhangshi")), EmailAddress = new("tonyzhangshi@163.com"), Avatar = new("0ACFC82E7D5A41FC8AB8FD4EF603C858Tony.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), LastLoginIp = new("201.182.1.23"), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new AccountNameVO("test01"), PasswordHash = new PasswordHashVO(new PasswordHashVO("111111").ToHash("test01")), EmailAddress = new("test01@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new AccountNameVO("test02"), PasswordHash = new PasswordHashVO(new PasswordHashVO("222222").ToHash("test02")), EmailAddress = new("test02@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(true), CreatedOn = dateTime },
       };
    }

}

