using System;
using System.Collections.Generic;

[Serializable]
public class CollectionSave
{
    public List<int> ids;

    public CollectionSave()
    {
        
    }
    
    public CollectionSave(List<int> ids)
    {
        this.ids = ids;
    }
}