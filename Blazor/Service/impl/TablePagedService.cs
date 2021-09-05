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

        public  TablePagedService(INamespaceAction<T> listService)
        {
            ListService = listService;
        }

        //public TablePagedService(IPodService podService)
        //{
        //    this.podService = podService;
        //}

        public IList<T> PagedItems;
        private IList<T> _originItems;

        private string _selectedNs = "";

        IEnumerable<T> selectedRows;

        public int PageIndex = 1;
        public int PageSize = 5;
        public int Total = 100;
        public bool Loading = false;
        private IPodService podService;

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

        public async void OnNsSelectedHandler(string ns)
        {
            Loading = true;
            _selectedNs = ns;
            //重置分页
            PageIndex = 1;
            await GetData(ns);
            Loading = false;
        }

       

        public async Task OnChange(QueryModel<T> queryModel)
        {
            Loading = true;
            PagedItems = _originItems.GetPagedTableData(queryModel);
            Loading = false;
        }

    }
}
