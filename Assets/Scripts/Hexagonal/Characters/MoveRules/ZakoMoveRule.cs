using System.Linq;
using UnityEngine;

public class ZakoMoveRule : MoveRule {
    public override void Move(EnemyController controller, Tile playerTile) {
        var nearTiles = controller.TileTransform.Tile.NearTiles.Values;
        var targetTile = nearTiles.ElementAt(Random.Range(0, nearTiles.Count));
        controller.TileTransform.MoveTo(targetTile);
    }
}
