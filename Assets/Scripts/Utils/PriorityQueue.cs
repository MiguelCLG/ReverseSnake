using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PriorityQueue<T>
{
    private SortedDictionary<float, Queue<T>> dictionary = new SortedDictionary<float, Queue<T>>();

    public void Enqueue(T item, float priority)
    {
        if (!dictionary.ContainsKey(priority))
        {
            dictionary[priority] = new Queue<T>();
        }
        dictionary[priority].Enqueue(item);
    }

    public T Dequeue()
    {
        var firstPair = dictionary.First();
        var items = firstPair.Value;
        T item = items.Dequeue();
        if (items.Count == 0)
        {
            dictionary.Remove(firstPair.Key);
        }
        return item;
    }

    public int Count
    {
        get
        {
            return dictionary.Sum(x => x.Value.Count);
        }
    }

    public bool Any() => Count > 0;

    // Novo método para verificar se um elemento já está na fila com qualquer prioridade
    public bool Contains(T item)
    {
        return dictionary.Any(pair => pair.Value.Contains(item));
    }
}