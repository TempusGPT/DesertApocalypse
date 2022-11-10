using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ZakoMoveRule : MoveRule {
    public override void Move(TileTransform tileTransform, Tile targetTile) {
        var nearTiles = tileTransform.Tile.NearWalkableTiles;
        var randomIndex = Random.Range(0, nearTiles.Count());
        var destinationTile = nearTiles.ElementAt(randomIndex);
        tileTransform.MoveTo(destinationTile).Forget();
    }
}