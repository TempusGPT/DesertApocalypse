using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class PlayerController : MonoBehaviour {
    public static event Action<Tile> OnMove;

    public TileTransform TileTransform { get; private set; }

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
    }

    private void Start() {
        Tile.OnClick += MoveToClickedTile;
        TileTransform.OnTileSet += NotifyMove;
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