using Microsoft.AspNetCore.Identity;

namespace Base.Contracts.Domain;

public interface IDomainAppUser<TKey, TAppUser> : IDomainAppUserId<TKey>
where TKey : IEquatable<TKey>
where TAppUser : IdentityUser<TKey>
{
  TAppUser? Author { get; set; }
}