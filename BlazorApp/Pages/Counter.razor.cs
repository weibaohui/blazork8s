using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages;

public partial class Counter : ComponentBase
{
    private int _currentCount;

    private void IncrementCount()
    {
        _currentCount += 1;
    }
}
