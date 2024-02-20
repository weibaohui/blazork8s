using System.Threading.Tasks;
using Entity;
using SqlSugar;

namespace BlazorApp.Service.k8s.impl;

public class DocService(ISqlSugarClient db) : IDocService
{
    public async Task<KubeExplainEntity> GetExplainByField(string fieldPath)
    {
        var rf = await db.Queryable<KubeExplainRef>().SingleAsync(it => it.ExplainFiled == fieldPath);
        if (rf == null)
        {
            return null;
        }

        var en = await db.Queryable<KubeExplainEN>().SingleAsync(it => it.Id == rf.EnId);
        var cn = await db.Queryable<KubeExplainCN>().SingleAsync(it => it.Id == rf.CnId);
        return new KubeExplainEntity
        {
            Explain      = en.Explain,
            ExplainCN    = cn.Explain,
            ExplainFiled = fieldPath
        };
    }
}
