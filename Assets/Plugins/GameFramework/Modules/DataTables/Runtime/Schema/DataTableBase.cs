using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataTableBase<T> : ScriptableObject where T : DataTableRowBase
{
    public List<T> Rows = new List<T>();
    private Dictionary<string, T> _runtimeCache;

    public void Initialize()
    {
        _runtimeCache = Rows.ToDictionary(r => r.Guid, r => r);
    }

    public T GetRowByGuid(string guid)
    {
        if (_runtimeCache == null)
        {
            Initialize();
        }
        return _runtimeCache.TryGetValue(guid, out var row) ? row as T : null;
    }

    public T GetRowByName(string name) => Rows.Find(r => r.RowName == name);
}
