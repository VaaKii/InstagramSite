using App.Domain;

namespace WebApp.ViewModels;

public class UserPostViewModel
{
    public PaginatedList<UserPost> Posts { get; set; } = default!;

}

public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize) : base(items)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}