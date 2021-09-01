using System.Collections.Generic;
using System.Linq;
using AntDesign.TableModels;

namespace Extension
{
    public static class TableData
    {
        public static IList<T> GetPagedTableData<T>(this IList<T> origin, QueryModel<T> queryModel)
        {
            var query = origin.Skip((queryModel.PageIndex - 1) * queryModel.PageSize).Take(queryModel.PageSize);
            return query.ToList();
        }
    }
}
