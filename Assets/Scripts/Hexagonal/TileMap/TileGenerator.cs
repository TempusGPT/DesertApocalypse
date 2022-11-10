using System.Linq;
using UnityEngine;

public class TileGenerator : MonoBehaviour {
    [SerializeField]
    private PlayerController playerPrefab;

    [SerializeField]
    private EnemyController zakoPrefab;

    [SerializeField]
    private EnemyController bossPrefab;

    [SerializeField]
    private Tile walkableTilePrefab;

    [SerializeField]
    private Tile nonWalkableTilePrefab;

    [SerializeField]
    private Vector2Int mapSize;

    [SerializeField]
    private Vector2Int[] nonWalkableTiles;

    private Tile[,] tileMap;

    private void Start() {
        tileMap = new Tile[mapSize.x, mapSize.y];
        InstantiateTiles();
        InitializeTiles();

        var player = Instantiate(playerPrefab);
        player.Initialize(tileMap[0, 0]);

        var zako = Instantiate(zakoPrefab);
        zako.Initialize(tileMap[mapSize.x - 1, 0]);

        var boss = Instantiate(bossPrefab);
        boss.Initialize(tileMap[mapSize.x - 1, mapSize.y - 1]);
    }

    private void InstantiateTiles() {
        var coord = new Vector2Int();
        for (coord.y = 0; coord.y < mapSize.y; coord.y++) {
            for (coord.x = 0; coord.x < mapSize.x; coord.x++) {
                var position = CalculatePosition(coord);
                var isWalkable = nonWalkableTiles.All(
                    tile => tile.x != coord.x || tile.y != coord.y
                );

                var tile = Instantiate(
                    isWalkable ? walkableTilePrefab : nonWalkableTilePrefab,
                    position,
                    Quaternion.identity,
                    transform
                );

                tile.name = $"Tile {coord}";
                tileMap[coord.x, coord.y] = tile;
            }
        }
    }

    private void InitializeTiles() {
        var coord = new Vector2Int();
        for (coord.y = 0; coord.y < mapSize.y; coord.y++) {
            for (coord.x = 0; coord.x < mapSize.x; coord.x++) {
                var tile = tileMap[coord.x, coord.y];
                tile.Initialize(tileMap, coord);
            }
        }
    }

    private static Vector3 CalculatePosition(Vector2Int coord) {
        var result = new Vector3(coord.x * 1.5f, coord.y * 1.5f, 0);
        if (coord.x % 2 == 1) {
            result.y -= 0.75f;
        }
        return result;
    }
}