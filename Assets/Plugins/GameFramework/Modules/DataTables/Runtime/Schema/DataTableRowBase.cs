using Unity.Collections;
using UnityEngine;

public abstract class DataTableRowBase : ScriptableObject
{
    [ReadOnly] public string Guid;
    public string RowName;
    [TextArea] public string Description;

    public virtual void OnCreated()
    {
        Guid = System.Guid.NewGuid().ToString();
    }
}
