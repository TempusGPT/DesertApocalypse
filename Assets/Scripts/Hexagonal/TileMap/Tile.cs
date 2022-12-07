using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler {
    public static event Action<Tile> OnClick;

    [SerializeField]
    private SpriteRenderer eyesightDisplay;

    [field: SerializeField]
    public bool IsWalkable { get; private set; }

    private readonly Dictionary<TileDirection, Tile> nearTilesMap = new();
    private Tween eyesightTween;

    public IEnumerable<Tile> NearWalkableTiles =>
        nearTilesMap.Values.Where(tile => tile.IsWalkable);

    private void Awake() {
        PlayerController.OnInitialize += HandleEyesightDisplay;
        PlayerController.OnMove += HandleEyesightDisplay;
    }

    public void Initialize(Tile[,] tileMap, Vector2Int coord) {
        var xEven = coord.x % 2 == 0;
        foreach (TileDirection direction in Enum.GetValues(typeof(TileDirection))) {
            var offset = DirectionToOffset(direction, xEven);
            AddNearTile(tileMap, coord, offset, direction);
        }
    }

    private void AddNearTile(
        Tile[,] tileMap,
        Vector2Int coord,
        Vector2Int offset,
        TileDirection direction
    ) {
        var result = coord + offset;
        if (result.x < 0 || result.x >= tileMap.GetLength(0) ||
            result.y < 0 || result.y >= tileMap.GetLength(1)) {
            return;
        }

        var nearTile = tileMap[result.x, result.y];
        nearTilesMap.Add(direction, nearTile);
    }

    private static Vector2Int DirectionToOffset(TileDirection direction, bool xEven) {
        return direction switch {
            TileDirection.Lower => new Vector2Int(0, -1),
            TileDirection.LowerLeft => new Vector2Int(-1, xEven ? 0 : -1),
            TileDirection.LowerRight => new Vector2Int(1, xEven ? 0 : -1),
            TileDirection.Upper => new Vector2Int(0, 1),
            TileDirection.UpperLeft => new Vector2Int(-1, xEven ? 1 : 0),
            TileDirection.UpperRight => new Vector2Int(1, xEven ? 1 : 0),
            _ => throw new ArgumentOutOfRangeException(
                nameof(direction),
                direction,
                null
            )
        };
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke(this);
    }

    private void HandleEyesightDisplay(Tile playerTile) {
        var playerNear = this == playerTile ||
            nearTilesMap.Values.Any(tile => tile == playerTile);

        eyesightTween?.Kill();
        eyesightTween = eyesightDisplay
            .DOFade(playerNear ? 0f : 0.75f, 3f)
            .SetSpeedBased();
    }

    public static float Distance(Tile a, Tile b) {
        return Vector2.Distance(
            a.transform.position,
            b.transform.position
        );
    }
}