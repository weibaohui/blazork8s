using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace ui.Pages
{
    public partial class FetchData:ComponentBase
    {
         WeatherForecast[] forecasts;
        [Inject]
        private HttpClient Http  { get; set; }

        protected override async Task  OnInitializedAsync()
        {
            forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("https://localhost:4001/WeatherForecast");
        }
        public class Data
        {
            [DisplayName("Key")]
            public string Key { get; set; }

            [DisplayName("Name")]
            public string Name { get; set; }

            [DisplayName("Age")]
            public int Age { get; set; }

            [DisplayName("Address")]
            public string Address { get; set; }

            [DisplayName("Tags")]
            public string[] Tags { get; set; }
        }

        static IEnumerable<Data> data = new Data[]
        {
            new()
            {
                Key = "1",
                Name = "John Brown",
                Age = 32,
                Address = "New York No. 1 Lake Park",
                Tags = new[] {"nice", "developer"}
            },
            new()
            {
                Key = "2",
                Name = "Jim Green",
                Age = 42,
                Address = "London No. 1 Lake Park",
                Tags = new[] { "loser"}
            },
            new()
            {
                Key = "3",
                Name = "Joe Black",
                Age = 32,
                Address = "Sidney No. 1 Lake Park",
                Tags = new[] { "cool", "teacher" }
            }
        }.ToList();

        private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };


    }
}