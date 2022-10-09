using UnityEngine;

public abstract class MoveRule : MonoBehaviour {
    public abstract void Move(TileTransform tileTransform, Tile targetTile);
}
