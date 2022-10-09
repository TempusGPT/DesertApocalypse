using System;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class EnemyController : MonoBehaviour {
    public static event Action OnBattleStart;

    [SerializeField]
    private MoveRule moveRule;

    public TileTransform TileTransform { get; private set; }

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
    }

    private void Start() {
        PlayerController.OnMove += HandlePlayerMove;
    }

    private void HandlePlayerMove(Tile playerTile) {
        if (playerTile == TileTransform.Tile) {
            OnBattleStart?.Invoke();
        } else {
            moveRule.Move(this, playerTile);
        }
    }
}
