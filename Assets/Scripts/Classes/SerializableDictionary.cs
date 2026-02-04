using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDictionaryPair<TKey,  TValue>
{
    TKey key     { get; }
    TValue value { get; set; }
}

[Serializable]
public abstract class SeriazableDictionary<TKey, TValue, TPair>
    where TPair : class, IDictionaryPair<TKey, TValue>, new()
{
    [SerializeField]
    private List<TPair> items = new List<TPair>();

    private Dictionary<TKey, TValue> _dictionary;
    public Dictionary<TKey, TValue> Dictionary
    {
        get
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<TKey, TValue>();

                foreach (var pair in items)
                {
                    if (pair != null)
                        _dictionary[pair.key] = pair.value;
                }
            }
            return _dictionary;
        }
    }
}