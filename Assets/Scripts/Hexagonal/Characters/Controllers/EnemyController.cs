using System;
using UnityEngine;

[RequireComponent(typeof(TileTransform))]
public class EnemyController : MonoBehaviour {
    public static event Action OnMeetPlayer;
    private bool _flag = false;

    [SerializeField]
    private MoveRule moveRule;

    private TileTransform TileTransform { get; set; }

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
        PlayerController.OnMove += HandlePlayerMove;

        OnMeetPlayer += SceneSwitcher.ChangeScene;
    }

    public void Initialize(Tile tile) {
        TileTransform.Tile = tile;
    }

    private void HandlePlayerMove(Tile playerTile) {
        PlayerController.CurrentTile = playerTile;
        if (playerTile == TileTransform.Tile && !_flag) {
            _flag = true;
            Debug.Log("Player met enemy");
            TileGenerator.tileParent.gameObject.SetActive(false);
            OnMeetPlayer?.Invoke();
        } else {
            moveRule.Move(TileTransform, playerTile);
        }
    }
}