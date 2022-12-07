using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class PlayerController : MonoBehaviour {
    public static event Action<Tile> OnInitialize;
    public static event Action<Tile> OnMove;

    private Transform cameraTransform;
    private TileTransform TileTransform { get; set; }

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
        Tile.OnClick += MoveToClickedTile;
        TileTransform.OnTileSet += NotifyMove;
    }

    private void LateUpdate() {
        if (cameraTransform is null) {
            return;
        }

        cameraTransform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            cameraTransform.position.z
        );
    }

    public void Initialize(Tile tile) {
        cameraTransform = Camera.main.transform;
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