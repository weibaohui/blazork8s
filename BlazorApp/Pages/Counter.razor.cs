using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorApp.Service;
using k8s.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages
{
    public partial class Counter : ComponentBase
    {
        private int currentCount = 0;
        private async Task IncrementCount()
        {
            currentCount += 1;
        }
    }
}
