using System;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class EnemyController : MonoBehaviour {
    public static event Action OnMeetPlayer;

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
            OnMeetPlayer?.Invoke();
        } else {
            moveRule.Move(TileTransform, playerTile);
        }
    }
}
