using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public static event Action<Tile> OnClick;

    [field: SerializeField]
    public bool IsWalkable { get; private set; }

    public Dictionary<TileDirection, Tile> NearTiles { get; } = new();

    public static int Distance(Tile a, Tile b) {
        return (int) Vector2.Distance(
            a.transform.position,
            b.transform.position
        );
    }

    private void OnMouseUp() {
        OnClick?.Invoke(this);
    }
}
