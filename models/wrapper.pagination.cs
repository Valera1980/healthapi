public class WrapperPagination<T>
{
    public int currentPage { get; set; }
    public int pageSize { get; set; }
    public int totalCount { get; set; }
    public T data { get; set; }
}