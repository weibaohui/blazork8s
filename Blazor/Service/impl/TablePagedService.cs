using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using Extension;
namespace Blazor.Service
{
    public class TablePagedService<T>
    {
        private readonly INamespaceAction<T> ListService;

        public TablePagedService(INamespaceAction<T> listService)
        {
            ListService = listService;
        }


        /// <summary>
        /// 页面显示的条目
        /// </summary>
        public IList<T> PagedItems;
        /// <summary>
        /// 获取的原始条目
        /// </summary>
        private IList<T> _originItems;

        private string _selectedNs;

        public IEnumerable<T> SelectedRows;

        public int PageIndex = 1;
        public int PageSize = 10;
        public int Total = 100;
        public bool Loading = false;

        void ChangePageSize(int pageSize)
        {
            if (PageSize != pageSize)
            {
                PageSize = pageSize;
                PageIndex = 1;
            }
        }

        public async Task GetData(string ns)
        {
            Loading = true;
            _originItems = await ListService.ListItemsByNamespaceAsync(ns);
            PagedItems = _originItems;
            Total = _originItems.Count;
            Loading = false;
        }

        /// <summary>
        /// 命名空间切换事件
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public async Task OnNsSelectedHandler(string ns)
        {
            if (_selectedNs == ns)
            {
                return;
            }
            Loading = true;
            PageIndex = 1;
            _selectedNs = ns;
            await GetData(ns);
            Loading = false;
        }


        /// <summary>
        /// 变更事件
        /// </summary>
        /// <param name="queryModel"></param>
        public void OnChange(QueryModel<T> queryModel)
        {
            Loading = true;
            var query = _originItems.Skip((queryModel.PageIndex - 1) * queryModel.PageSize).Take(queryModel.PageSize);

            PagedItems = query.ToList();
            Loading = false;
        }
    }
}
