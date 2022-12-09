using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TileTransform))]
public class PlayerController : MonoBehaviour {
    public static event Action<Tile> OnInitialize;
    public static event Action<Tile> OnMove;

    private Transform cameraTransform;
    public TileTransform TileTransform { get; set; }
    private Image gaugeImage;
    public static Animator anim;
    public static Tile CurrentTile;

    private void Awake() {
        TileTransform = GetComponent<TileTransform>();
        Tile.OnClick += MoveToClickedTile;
        TileTransform.OnTileSet += NotifyMove;
        gaugeImage = GameObject.Find("Gauge").GetComponent<Image>();
        gaugeImage.fillAmount = (float)PlayerControl.PlayerHP / 100f;
        anim = GetComponentInChildren<Animator>();
    }

    private void LateUpdate() {
        if (cameraTransform is null) {
            return;
        }

        cameraTransform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            cameraTransform.position.z
        );
    }

    public void Initialize(Tile tile) {
        cameraTransform = Camera.main.transform;
        TileTransform.Tile = tile;
        OnInitialize?.Invoke(tile);
    }

    private void MoveToClickedTile(Tile tile) {
        if (!tile.IsWalkable) {
            return;
        }
        anim?.SetBool("isRunning", true);
        TileTransform.MoveTo(tile).Forget();
    }

    private static void NotifyMove(Tile tile) {
        OnMove?.Invoke(tile);
    }
}