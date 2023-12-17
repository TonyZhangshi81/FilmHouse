
using System;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmHouse.Data.Spec;

public sealed class UserAccountSpec : BaseSpecification<UserAccountEntity>
{
    public UserAccountSpec(AccountNameVO account) : base(c => c.Account == account)
    {
    }

}