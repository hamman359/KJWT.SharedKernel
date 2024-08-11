using System.Text.Json.Serialization;

namespace KJWT.SharedKernel.Results;

public class PagedInfo
{
    public PagedInfo(long pageNumer, long pageSize, long totalPages, long totalRecords)
    {
        PageNumber = pageNumer;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalRecords = totalRecords;
    }

    [JsonInclude]
    public long PageNumber { get; private set; }

    [JsonInclude]
    public long PageSize { get; private set; }

    [JsonInclude]
    public long TotalPages { get; private set; }

    [JsonInclude]
    public long TotalRecords { get; private set; }


    public PagedInfo SetPageNumber(long pageNumber)
    {
        PageNumber = pageNumber;

        return this;
    }

    public PagedInfo SetPageSize(long pageSize)
    {
        PageSize = pageSize;

        return this;
    }

    public PagedInfo SetTotalPages(long totalPages)
    {
        TotalPages = totalPages;

        return this;
    }

    public PagedInfo SetTotalRecords(long totalRecords)
    {
        TotalRecords = totalRecords;

        return this;
    }
}