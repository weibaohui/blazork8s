using Microsoft.AspNetCore.Components;

namespace ui.Pages
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
