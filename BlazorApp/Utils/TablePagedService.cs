using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.TableModels;
using BlazorApp.Service;
using k8s;
using k8s.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp.Utils;

public class TablePagedService<T> where T : IKubernetesObject<V1ObjectMeta>
{
    public TablePagedService()
    {
    }


    /// <summary>
    /// 页面显示的条目
    /// </summary>
    public IList<T> PagedItems;

    /// <summary>
    /// 获取的原始条目
    /// </summary>
    public IList<T> OriginItems;

    private string _selectedNs;

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

    public async Task GetData(string ns)
    {
        Loading    = true;
        PagedItems = OriginItems;
        Total      = OriginItems.Count;
        PageIndex  = 1;
        Loading    = false;
    }

    public async Task CopyData(ResourceCache<T> data)
    {
        Loading     = true;
        OriginItems = data.Get();
        PagedItems  = OriginItems;
        Total       = PagedItems.Count;
        PageIndex   = 1;
        await OnNsSelectedHandler(_selectedNs);
        Loading = false;
    }

    /// <summary>
    /// 命名空间切换事件
    /// </summary>
    /// <param name="ns"></param>
    /// <returns></returns>
    public async Task OnNsSelectedHandler(string ns)
    {
        Loading     = true;
        PageIndex   = 1;
        _selectedNs = ns;
        PagedItems  = OriginItems;

        if (!ns.IsNullOrEmpty())
        {
            PagedItems = PagedItems.Where(x => x.Namespace() == ns).ToList();
        }

        Total   = PagedItems.Count;
        Loading = false;
    }


    /// <summary>
    /// 变更事件
    /// </summary>
    /// <param name="queryModel"></param>
    public async Task OnChange(QueryModel<T> queryModel)
    {
        await OnNsSelectedHandler(_selectedNs);
        PageIndex = queryModel.PageIndex;
        PageSize  = queryModel.PageSize;
        Loading   = true;
        var query = PagedItems.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        PagedItems = query.ToList();
        Loading    = false;
    }

    public async Task SearchName(string key)
    {
        //TODO key 作为一个变量，类似ns处理
        await OnNsSelectedHandler(_selectedNs);
        Loading   = true;
        PageIndex = 1;

        if (!key.IsNullOrEmpty())
        {
            PagedItems = PagedItems.Where(w => w.Name().Contains(key)).ToList();
        }

        Total   = PagedItems.Count();
        Loading = false;
    }
}
