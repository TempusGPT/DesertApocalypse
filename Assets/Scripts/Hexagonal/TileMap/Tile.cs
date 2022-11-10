using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler {
    public static event Action<Tile> OnClick;

    [SerializeField]
    private GameObject eyesightDisplay;

    [field: SerializeField]
    public bool IsWalkable { get; private set; }

    public Dictionary<TileDirection, Tile> NearTilesMap { get; } = new();

    public IEnumerable<Tile> NearTiles => NearTilesMap.Values;
    public IEnumerable<Tile> NearWalkableTiles => NearTiles.Where(tile => tile.IsWalkable);

    private void Start() {
        PlayerController.OnMove += Foo;
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke(this);
    }

    public static float Distance(Tile a, Tile b) {
        return Vector2.Distance(
            a.transform.position,
            b.transform.position
        );
    }

    private void Foo(Tile playerTile) {
        var playerNear = this == playerTile || NearTiles.Any(tile => tile == playerTile);
        eyesightDisplay.SetActive(!playerNear);
    }
}