namespace NP.DataTemplates.Interfaces
{
    public interface IPagingSpecification
    {
        int PageNo { get; }

        int PageSize { get; }
    }
}