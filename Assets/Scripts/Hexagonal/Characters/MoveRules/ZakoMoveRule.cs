using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ZakoMoveRule : MoveRule {
    public override void Move(TileTransform tileTransform, Tile targetTile) {
        var nearTiles = tileTransform.Tile.NearTiles.Values;
        var destinationTile = nearTiles.ElementAt(Random.Range(0, nearTiles.Count));
        tileTransform.MoveTo(destinationTile).Forget();
    }
}