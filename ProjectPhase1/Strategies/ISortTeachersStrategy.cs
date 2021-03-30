using System.Collections.Generic;

namespace ProjectPhase1.Strategies
{
    public interface ISortTeachersStrategy
    {
        List<Teacher> SortByAsc(IEnumerable<Teacher> teachers);
        List<Teacher> SortByDesc(IEnumerable<Teacher> teachers);
    }
}