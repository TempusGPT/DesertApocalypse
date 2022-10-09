using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TileTransform : MonoBehaviour {
    public event Action<Tile> OnTileSet;

    private const float MoveDuration = 0.5f;
    private readonly Pathfinder pathfinder = new();
    private Tile targetTile;

    [field: SerializeField]
    public Tile Tile { get; private set; }

    private void Awake() {
        transform.position = Tile.transform.position;
    }

    public async UniTask MoveTo(Tile destinationTile) {
        if (targetTile != null) {
            targetTile = destinationTile;
            return;
        }
        
        targetTile = destinationTile;
        while (targetTile != null) {
            await MoveToDestination();
        }
    }

    private async UniTask MoveToDestination() {
        var destinationTile = targetTile;
        foreach (var pathTile in pathfinder.Find(Tile, destinationTile)) {
            await SetTile(pathTile);
            if (targetTile != destinationTile) {
                return;
            }
        }
        targetTile = null;
    }

    private async UniTask SetTile(Tile value) {
        await transform
            .DOMove(value.transform.position, MoveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Tile = value;
                OnTileSet?.Invoke(value);
            })
            .AsyncWaitForCompletion();
    }
}
