using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler {
    public static event Action<Tile> OnClick;

    [field: SerializeField]
    public bool IsWalkable { get; private set; }

    public Dictionary<TileDirection, Tile> NearTiles { get; } = new();

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke(this);
    }

    public static float Distance(Tile a, Tile b) {
        return Vector2.Distance(
            a.transform.position,
            b.transform.position
        );
    }
}