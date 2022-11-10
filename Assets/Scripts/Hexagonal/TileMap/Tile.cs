using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Tile : MonoBehaviour, IPointerClickHandler {
    public static event Action<Tile> OnClick;

    [SerializeField]
    private GameObject eyesightDisplay;

    [field: SerializeField]
    public bool IsWalkable { get; private set; }

    private readonly Dictionary<TileDirection, Tile> nearTilesMap = new();

    public IEnumerable<Tile> NearWalkableTiles =>
        nearTilesMap.Values.Where(tile => tile.IsWalkable);

    private void Awake() {
        PlayerController.OnInitialize += HandleEyesightDisplay;
        PlayerController.OnMove += HandleEyesightDisplay;
    }

    public void Initialize(Tile[,] tileMap, Vector2Int coord) {
        Initializer.Initialize(this, tileMap, coord);
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke(this);
    }

    private void HandleEyesightDisplay(Tile playerTile) {
        var playerNear = this == playerTile ||
            nearTilesMap.Values.Any(tile => tile == playerTile);
        eyesightDisplay.SetActive(!playerNear);
    }

    public static float Distance(Tile a, Tile b) {
        return Vector2.Distance(
            a.transform.position,
            b.transform.position
        );
    }
}