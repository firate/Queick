namespace AppointmentService;

public class PaginationResult<T>
{
    public IEnumerable<T> Items{ get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page > 1;
    public bool HasPreviousPage  => Page < TotalPages;
    
    public PaginationResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }
}