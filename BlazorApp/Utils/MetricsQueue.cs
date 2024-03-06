using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Utils;

public class MetricsQueue<T>
{
    private const    int                MetricCount = 60;
    private readonly ConcurrentQueue<T> _queue      = new();

    public void Enqueue(T item)
    {
        if (_queue.Count > MetricCount)
        {
            for (var i = 0; i < _queue.Count - MetricCount; i++)
            {
                _queue.TryDequeue(out _);
            }
        }

        _queue.Enqueue(item);
    }

    public List<T> GetList()
    {
        return _queue.ToList();
    }
}
