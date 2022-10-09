using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class PlayerController : MonoBehaviour, ITileObject {
    public static PlayerController Instance { get; private set; }
    public TileTransform TileTransform { get; private set; }

    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        TileTransform = GetComponent<TileTransform>();
    }

    private void Start() {
        Tile.OnClick += MoveToClickedTile;
    }

    private void MoveToClickedTile(Tile tile) {
        if (!tile.IsWalkable) {
            return;
        }
        TileTransform.MoveTo(tile);
    }
}
