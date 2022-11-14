using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour {
    [SerializeField]
    private Vector2Int mapSize;
    private Tile[,] tileMap;

    private PlayerController playerPrefab;
    private EnemyController zakoPrefab;
    private EnemyController bossPrefab;
    private Tile walkableTilePrefab;
    private Tile nonWalkableTilePrefab;

    private void Awake() {
        playerPrefab = Resources.Load<PlayerController>("Hexagonal/PlayerCharacter");
        zakoPrefab = Resources.Load<EnemyController>("Hexagonal/ZakoEnemy");
        bossPrefab = Resources.Load<EnemyController>("Hexagonal/BossEnemy");
        walkableTilePrefab = Resources.Load<Tile>("Hexagonal/WalkableTile");
        nonWalkableTilePrefab = Resources.Load<Tile>("Hexagonal/NonWalkableTile");
    }

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
                var isWalkable = Random.Range(0, 2) == 0;
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