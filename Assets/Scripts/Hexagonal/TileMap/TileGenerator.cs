using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour {
    [SerializeField]
    private Vector2Int mapSize;

    [SerializeField]
    private int phaseCount;

    private static Tile[,] tileMap;
    private static bool isTileInitialized = false;

    private PlayerController playerPrefab;
    private EnemyController zakoPrefab;
    private EnemyController bossPrefab;
    private Tile walkableTilePrefab;
    private Tile nonWalkableTilePrefab;
    private static List<Vector2Int> bossCoords;

    private void Awake() {
        if (tileMap == null)
            tileMap = new Tile[mapSize.x * phaseCount + phaseCount + 1, mapSize.y];
        playerPrefab = Resources.Load<PlayerController>("Hexagonal/PlayerCharacter");
        zakoPrefab = Resources.Load<EnemyController>("Hexagonal/ZakoEnemy");
        bossPrefab = Resources.Load<EnemyController>("Hexagonal/BossEnemy");
        walkableTilePrefab = Resources.Load<Tile>("Hexagonal/WalkableTile");
        nonWalkableTilePrefab = Resources.Load<Tile>("Hexagonal/NonWalkableTile");
    }

    private void Start() {
        var playerCoord = InstantiatePass(Vector2Int.zero);
        if (PlayerController.CurrentTile == null) {
            PlayerController.CurrentTile = tileMap[playerCoord.x, playerCoord.y];
        }
        
        if (bossCoords == null) {
            bossCoords = InstantiateMap();
        }
        else if (bossCoords.Count > 0) {
            bossCoords.RemoveAt(0);
        }

        if (!isTileInitialized) {
            isTileInitialized = true;
            InitializeMap();
        }

        Instantiate(playerPrefab).Initialize(PlayerController.CurrentTile);
        foreach (var bossCoord in bossCoords) {
            Instantiate(bossPrefab).Initialize(tileMap[bossCoord.x, bossCoord.y]);
        }
    }

    private List<Vector2Int> InstantiateMap() {
        var bossCoords = new List<Vector2Int>();
        var offset = Vector2Int.zero;

        for (var i = 0; i < phaseCount; i++) {
            Debug.Log(offset);
            offset.x += 1;
            InstantiateHalf(offset);
            offset.x += mapSize.x;
            bossCoords.Add(InstantiatePass(offset));
        }

        return bossCoords;
    }

    private void InstantiateHalf(Vector2Int offset) {
        for (var coord = Vector2Int.zero; coord.y < mapSize.y; coord.y++) {
            for (coord.x = 0; coord.x < mapSize.x; coord.x++) {
                var position = CalculatePosition(coord + offset);
                var isWalkable = Random.value <= 0.75f;
                var tile = Instantiate(
                    isWalkable ? walkableTilePrefab : nonWalkableTilePrefab,
                    position,
                    Quaternion.identity,
                    transform
                );

                tile.name = $"Tile {coord + offset}";
                tileMap[coord.x + offset.x, coord.y + offset.y] = tile;
            }
        }
    }

    private Vector2Int InstantiatePass(Vector2Int offset) {
        var spawnCoord = new Vector2Int(0, Random.Range(0, mapSize.y));
        for (var coord = Vector2Int.zero; coord.y < mapSize.y; coord.y++) {
            var position = CalculatePosition(coord + offset);
            var isWalkable = coord == spawnCoord;
            var tile = Instantiate(
                isWalkable ? walkableTilePrefab : nonWalkableTilePrefab,
                position,
                Quaternion.identity,
                transform
            );

            tile.name = $"Tile {coord + offset}";
            tileMap[coord.x + offset.x, coord.y + offset.y] = tile;
        }
        return spawnCoord + offset;
    }

    private void InitializeMap() {
        for (var coord = Vector2Int.zero; coord.y < tileMap.GetLength(1); coord.y++) {
            for (coord.x = 0; coord.x < tileMap.GetLength(0); coord.x++) {
                var tile = tileMap[coord.x, coord.y];
                tile.Initialize(tileMap, coord);
            }
        }
    }

    private static Vector3 CalculatePosition(Vector2Int coord) {
        var result = new Vector3(coord.x * 0.58f, coord.y * 0.64f, 0);
        if (coord.x % 2 == 1) {
            result.y -= 0.32f;
        }
        return result;
    }
}