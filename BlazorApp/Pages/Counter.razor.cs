using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages;

public partial class Counter : ComponentBase
{
    private int _currentCount = 0;

    private void IncrementCount()
    {
        _currentCount += 1;
    }
}
