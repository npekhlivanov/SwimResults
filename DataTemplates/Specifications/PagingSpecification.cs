using NP.DataTemplates.Interfaces;

namespace NP.DataTemplates.Specifications
{
    public class PagingSpecification : IPagingSpecification
    {
        public PagingSpecification(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }

        public int PageNo { get; }

        public int PageSize { get; }
    }
}
