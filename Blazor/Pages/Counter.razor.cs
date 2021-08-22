using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class Counter : ComponentBase
    {
        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount += 1;
        }
    }
}
