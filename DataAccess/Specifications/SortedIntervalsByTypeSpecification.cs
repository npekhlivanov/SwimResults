using System;
using System.Linq.Expressions;
using DataModels;
using Microsoft.EntityFrameworkCore;
using NP.DataTemplates.Specifications;

namespace DataAccess.Specifications
{
    public class SortedIntervalsByTypeSpecification : BaseQuerySpecification<WorkoutInterval>
    {
        public SortedIntervalsByTypeSpecification(
            int selectedIntervalType,
            Expression<Func<WorkoutInterval, object>> sortSelector,
            bool sortDescending,
            int pageNo,
            int pageSize)
            : base(
                  i => i.WorkoutIntervalTypeId == selectedIntervalType,
                  new IncludeSpecification<WorkoutInterval>(q => q
                    .Include(i => i.WorkoutIntervalType)
                    .Include(i => i.Workout)),
                  new SortOrderSpecification<WorkoutInterval>(sortSelector, sortDescending),
                  new PagingSpecification(pageNo, pageSize))
        {
        }

        //public class IntervalsByTypeSpecification : BaseSpecification<WorkoutInterval>
        //{
        //    public IntervalsByTypeSpecification(int selectedIntervalType, Expression<Func<WorkoutInterval, object>> sortSelector, bool sortDescending, int pageNo, int pageSize) 
        //        : base(i => i.WorkoutIntervalTypeId == selectedIntervalType, 
        //              new SortOrderSpecification<WorkoutInterval>(sortSelector, sortDescending), 
        //              new PagingSpecification(pageNo, pageSize))
        //    {
        //        AddInclude(i => i.WorkoutIntervalType);
        //        AddInclude(i => i.Workout);
        //    }
    }
}
