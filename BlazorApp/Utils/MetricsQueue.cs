using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Utils;

public class MetricsQueue<T>
{
    private readonly Queue<T> _queue = new(10);

    public void Enqueue(T item)
    {
        if (_queue.Count >= 10)
        {
            for (int i = 0; i < _queue.Count - 10; i++)
            {
                _queue.Dequeue();
            }
        }

        _queue.Enqueue(item);
    }

    public List<T> GetList()
    {
        return _queue.ToList();
    }
}
