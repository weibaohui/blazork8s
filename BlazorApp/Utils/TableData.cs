using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using k8s;
using k8s.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Utils;

public class TableData<T> where T : IKubernetesObject<V1ObjectMeta>
{
    /// <summary>
    /// 页面显示的条目
    /// </summary>
    public IList<T> PagedItems;

    /// <summary>
    /// 获取的原始条目
    /// </summary>
    private List<T> _originItems;

    private string _selectedNs;
    private string _searchKey;

    public IEnumerable<T> SelectedRows;

    public int  PageIndex = 1;
    public int  PageSize  = 10;
    public int  Total     = 100;
    public bool Loading   = false;

    void ChangePageSize(int pageSize)
    {
        if (PageSize == pageSize) return;
        PageSize  = pageSize;
        PageIndex = 1;
    }

    public void CopyData(ResourceCache<T> data)
    {
        _originItems = data.Get().ToList();
        _originItems.Sort((x, y) => y.CreationTimestamp()?.CompareTo(x.CreationTimestamp()) ?? 0);
        PagedItems = _originItems;
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

    public string GetCurrentSearchKey()
    {
        return _searchKey;
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

    public void RemoveSelection(string ns, string name)
    {
        var tmp = SelectedRows.ToList();
        tmp.RemoveAll(x => x.Name() == name && x.Namespace() == ns);
        SelectedRows = tmp;
    }

    public void RemoveSelection(string nsAndName)
    {
        var tmp = SelectedRows.ToList();

        nsAndName = nsAndName.Trim();
        if (nsAndName.StartsWith('/'))
        {
            //说明是cluster级别，不区分ns的
            var name = nsAndName.Replace("/", "");
            // key 为name，cluster级别的资源
            tmp.RemoveAll(x => x.Name() == name);
            SelectedRows = tmp;
        }
        else
        {
            var strings = nsAndName.Split("/");
            if (strings.Length != 2) return;
            // key为ns/name 格式
            var ns   = strings[0];
            var name = strings[1];
            tmp.RemoveAll(x => x.Name() == name && x.Namespace() == ns);
            SelectedRows = tmp;
        }
    }

    public void RemoveAllSelection()
    {
        SelectedRows = new List<T>();
    }
}
