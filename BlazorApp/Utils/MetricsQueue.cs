using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Utils;

public class MetricsQueue<T>
{
    private const    int      MetricCount = 60;
    private readonly Queue<T> _queue      = new(MetricCount);

    public void Enqueue(T item)
    {
        if (_queue.Count > MetricCount)
        {
            for (int i = 0; i < _queue.Count - MetricCount; i++)
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
