using System;
using Microsoft.Extensions.Configuration;

namespace ui.Service
{
    public class BlogService
    {
      private IConfiguration Configuration { get; set; }

        

        public String GetBaseApiURL()
        {
            var url = Configuration.GetSection("ClientAppSettings").GetValue<String>("BaseApiUrl");
            Console.WriteLine(url+"GetBaseApiURL");
            return url;
        }
    }
}