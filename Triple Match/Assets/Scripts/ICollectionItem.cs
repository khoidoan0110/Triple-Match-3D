using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectionItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnCollect();
}

public class CollectionEventArgs : EventArgs
{
    public ICollectionItem Item { get; }

    public CollectionEventArgs(ICollectionItem item)
    {
        Item = item;
    }
}
