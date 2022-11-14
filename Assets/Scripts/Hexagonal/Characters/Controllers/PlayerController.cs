using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class PlayerController : MonoBehaviour, IInitializableController {
    public static event Action<Tile> OnInitialize;
    public static event Action<Tile> OnMove;

    private TileTransform TileTransform { get; set; }

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
        Tile.OnClick += MoveToClickedTile;
        TileTransform.OnTileSet += NotifyMove;
    }

    public void Initialize(Tile tile) {
        TileTransform.Tile = tile;
        OnInitialize?.Invoke(tile);
    }

    private void MoveToClickedTile(Tile tile) {
        if (!tile.IsWalkable) {
            return;
        }
        TileTransform.MoveTo(tile).Forget();
    }

    private static void NotifyMove(Tile tile) {
        OnMove?.Invoke(tile);
    }
}