using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class EnemyController : MonoBehaviour {
    public TileTransform TileTransform { get; private set; }

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
    }

    private void Start() {
        PlayerController.Instance.TileTransform.OnTileSet += MoveToNearTile;
    }

    private void MoveToNearTile() {
        var nearTiles = TileTransform.Tile.NearTiles.Values;
        var targetTile = nearTiles.ElementAt(Random.Range(0, nearTiles.Count));
        TileTransform.MoveTo(targetTile);
    }
}
