using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public static event Action<Tile> OnClick;

    [field: SerializeField]
    public bool IsWalkable { get; private set; }

    public Dictionary<TileDirection, Tile> NearTiles { get; } = new();

    private void OnMouseUp() {
        OnClick?.Invoke(this);
    }
}
