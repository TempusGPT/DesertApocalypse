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
    public IEnumerable<Tile> NearWalkableTiles =>
        NearTilesMap.Values.Where(tile => tile.IsWalkable);

    private void Awake() {
        PlayerController.OnInitialize += HandleEyesightDisplay;
        PlayerController.OnMove += HandleEyesightDisplay;
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke(this);
    }

    private void HandleEyesightDisplay(Tile playerTile) {
        var playerNear = this == playerTile ||
            NearTilesMap.Values.Any(tile => tile == playerTile);
        eyesightDisplay.SetActive(!playerNear);
    }

    public static float Distance(Tile a, Tile b) {
        return Vector2.Distance(
            a.transform.position,
            b.transform.position
        );
    }
}