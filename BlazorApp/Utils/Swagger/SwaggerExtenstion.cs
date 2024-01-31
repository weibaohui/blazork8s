using System;
using Entity;

namespace BlazorApp.Utils.Swagger;

public static class SwaggerExtenstion
{
    public static TreeData ToTreeData(this Definition item)
    {
        TreeData td = new TreeData();
        td.Title       = item.name;
        td.description = item.description;
        td.type        = item.type;
        td.format      = item.format;
        td.descriptionCN = item.descriptionCN;
        if (item.properties == null) return td;
        foreach (var (key, p) in item.properties)
        {
            td.ChildList.Add(new TreeData()
            {
                Title       = key,
                type        = p.type,
                description = p.description,
                format      = p.format,
                RefKey      = p.Ref ?? p.items?.Ref,
                descriptionCN = p.descriptionCN
            });
        }

        td.ChildList.Sort((a, b) => String.Compare(a.Title, b.Title, StringComparison.Ordinal));
        return td;
    }
}
