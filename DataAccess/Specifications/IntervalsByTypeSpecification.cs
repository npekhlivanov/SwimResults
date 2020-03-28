namespace DataAccess.Specifications
{
    using DataModels;
    using NP.DataTemplates.Specifications;

    public class IntervalsByTypeSpecification : BaseQuerySpecification<WorkoutInterval>
    {
        public IntervalsByTypeSpecification(int selectedIntervalType) : base(
            i => i.WorkoutIntervalTypeId == selectedIntervalType, 
            null, 
            null, 
            null)
        {
        }
    }
}
