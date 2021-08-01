using System.Threading.Tasks;
using AntDesign.ProLayout;
namespace ui
{
    public partial class BasicLayout
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public readonly MenuDataItem[] _menuData =
        {
            new MenuDataItem
            {
                Path = "/",
                Name = "welcome",
                Key  = "welcome",
                Icon = "smile",
            },
            new MenuDataItem
            {
                Path = "/NodeList",
                Name = "NodeList",
                Key  = "NodeList",
                Icon = "smile",
            }, new MenuDataItem
            {
                Path = "/fetchdata",
                Name = "fetchdata",
                Key  = "fetchdata",
                Icon = "smile",
            }, new MenuDataItem
            {
                Path = "/counter",
                Name = "Counter",
                Key  = "Counter",
                Icon = "smile",
            }
        };

        public LinkItem[] Links { get; set; } =
        {
            new LinkItem
            {
                Key         = "Ant Design Blazor",
                Title       = "Ant Design Blazor",
                Href        = "https://antblazor.com",
                BlankTarget = true,
            },
            new LinkItem
            {
                Key         = "github",
                Title       = "github",
                Href        = "https://github.com/ant-design-blazor/ant-design-pro-blazor",
                BlankTarget = true,
            },
            new LinkItem
            {
                Key         = "Blazor",
                Title       = "Blazor",
                Href        = "https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor?WT.mc_id=DT-MVP-5003987",
                BlankTarget = true,
            }
        };

    }
}
