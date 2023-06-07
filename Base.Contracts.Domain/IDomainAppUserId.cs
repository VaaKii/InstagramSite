namespace Base.Contracts.Domain;

public interface IDomainAppUserId<TKey>
where TKey : IEquatable<TKey>
{
  TKey AuthorId { get; set; }
}