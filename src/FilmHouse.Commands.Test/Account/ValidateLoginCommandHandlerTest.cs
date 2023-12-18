using FilmHouse.Commands.Test.Utils;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data;
using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using NUnit.Framework;

namespace FilmHouse.Commands.Test.Account;

[TestFixture]
public class ValidateLoginCommandHandlerTest : TransactionalTestBase
{
    [Test]
    public async Task MyTest1()
    {
        var uuid = new RequestIdVO(Guid.NewGuid());
        var sysDate = new CreatedOnVO(System.DateTime.Now);

        await this.DbContext.UserAccounts.AddRangeAsync(this.GetUserAccounts(uuid, sysDate));
        //this._dbContext.SaveChanges();

        Assert.That(this.DbContext.UserAccounts.Count(), Is.EqualTo(3));

    }
    [Test]
    public void MyTest2()
    {
        Assert.That(this.DbContext.UserAccounts.Count(), Is.EqualTo(3));

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

