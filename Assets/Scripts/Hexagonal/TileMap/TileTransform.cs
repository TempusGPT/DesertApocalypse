using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TileTransform : MonoBehaviour {
    public event Action OnTileSet;

    private const float MoveDuration = 0.5f;

    [field: SerializeField]
    public Tile Tile { get; private set; }

    private void Awake() {
        transform.position = Tile.transform.position;
    }

    public async UniTask MoveTo(Tile targetTile) {
        foreach (var pathTile in Pathfinder.Find(Tile, targetTile)) {
            await SetTile(pathTile);
        }
    }

    private async UniTask SetTile(Tile value) {
        await transform
            .DOMove(value.transform.position, MoveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Tile = value;
                OnTileSet?.Invoke();
            })
            .AsyncWaitForCompletion();
    }
}
