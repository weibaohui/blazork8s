using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Service;
using k8s;
using k8s.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Utils;

public class TableDataHelper<T> where T : IKubernetesObject<V1ObjectMeta>
{
    /// <summary>
    /// 页面显示的条目
    /// </summary>
    public IList<T> PagedItems;

    /// <summary>
    /// 获取的原始条目
    /// </summary>
    private IList<T> _originItems;

    private string _selectedNs;
    private string _searchKey;

    public IEnumerable<T> SelectedRows;

    public int  PageIndex = 1;
    public int  PageSize  = 5;
    public int  Total     = 100;
    public bool Loading   = false;

    void ChangePageSize(int pageSize)
    {
        if (PageSize == pageSize) return;
        PageSize  = pageSize;
        PageIndex = 1;
    }

    public void CopyData(ResourceCacheHelper<T> data)
    {
        _originItems = data.Get();
        PagedItems   = _originItems;
        PageIndex    = 1;
        FilterNsAndKey();
    }

    /// <summary>
    /// 命名空间切换事件
    /// </summary>
    /// <param name="ns"></param>
    /// <returns></returns>
    public void OnNsSelectedHandler(string ns)
    {
        _selectedNs = ns;
        FilterNsAndKey();
    }


    /// <summary>
    /// 变更事件
    /// </summary>
    /// <param name="queryModel"></param>
    public void OnPageChange(QueryModel<T> queryModel)
    {
        FilterNsAndKey();
        PageIndex  = queryModel.PageIndex;
        PageSize   = queryModel.PageSize;
        Loading    = true;
        PagedItems = PagedItems.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        Loading    = false;
    }

    public void OnSearchKeyChanged(string key)
    {
        _searchKey = key;
        FilterNsAndKey();
    }

    private void FilterNsAndKey()
    {
        Loading    = true;
        PagedItems = _originItems;
        FilterSearchKey();
        FilterNs();
        Total   = PagedItems.Count;
        Loading = false;
    }


    /// <summary>
    /// 过滤Namespace
    /// </summary>
    private void FilterNs()
    {
        if (!_selectedNs.IsNullOrEmpty())
        {
            PagedItems = PagedItems.Where(x => x.Namespace() == _selectedNs).ToList();
        }
    }

    /// <summary>
    /// 过滤Namespace
    /// </summary>
    private void FilterSearchKey()
    {
        if (!_searchKey.IsNullOrEmpty())
        {
            PagedItems = PagedItems.Where(w => w.Name().Contains(_searchKey)).ToList();
        }
    }
}
