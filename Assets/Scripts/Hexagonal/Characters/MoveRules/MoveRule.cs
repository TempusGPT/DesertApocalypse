using UnityEngine;

public abstract class MoveRule : MonoBehaviour {
    public abstract void Move(EnemyController controller, Tile playerTile);
}
