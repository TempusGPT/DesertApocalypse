using System;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class EnemyController : MonoBehaviour {
    public static event Action OnMeetPlayer;

    [SerializeField]
    private MoveRule moveRule;

    private TileTransform TileTransform { get; set; }

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
        PlayerController.OnMove += HandlePlayerMove;
    }

    public void Initialize(Tile tile) {
        TileTransform.Tile = tile;
    }

    private void HandlePlayerMove(Tile playerTile) {
        if (playerTile == TileTransform.Tile) {
            Debug.Log("Player met enemy");
            OnMeetPlayer?.Invoke();
        } else {
            moveRule.Move(TileTransform, playerTile);
        }
    }
}