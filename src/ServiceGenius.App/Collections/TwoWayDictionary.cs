using System.Collections;
using System.Collections.Generic;

namespace ServiceGenius.App.Collections;

public class TwoWayDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
{
    private readonly Dictionary<T1, T2> dic1;
    private readonly Dictionary<T2, T1> dic2;

    public TwoWayDictionary()
    {
        dic1 = [];
        dic2 = [];
    }

    public T2 this[T1 key, bool forward = true]
    {
        get => dic1[key];
        set
        {
            try
            {
                dic1[key] = value;
                dic2[value] = key;
            }
            catch
            {
                dic1.Remove(key);
                dic2.Remove(value);
                throw;
            }
        }
    }

    public T1 this[T2 key]
    {
        get => dic2[key];
        set => this[value, true] = key;
    }

    public IEnumerable<T1> Type1Values => dic1.Keys;
    public IEnumerable<T2> Type2Values => dic2.Keys;

    public void Add(T1 key, T2 value)
    {
        try
        {
            dic1.Add(key, value);
            dic2.Add(value, key);
        }
        catch
        {
            dic1.Remove(key);
            dic2.Remove(value);
            throw;
        }
    }

    public bool TryAdd(T1 key, T2 value)
    {
        try
        {
            if (dic1.ContainsKey(key) || dic2.ContainsKey(value))
            {
                return false;
            }

            Add(key, value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool TryGetValue(T1 value, out T2 result) => dic1.TryGetValue(value, out result);
    public bool TryGetValue(T2 value, out T1 result) => dic2.TryGetValue(value, out result);
    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
    {
        foreach (KeyValuePair<T1, T2> par in dic1)
        {
            yield return par;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}