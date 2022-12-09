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

    private Tile[,] tileMap;
    private static bool isTileInitialized = false;
    public static bool ReGenerate = false;

    private PlayerController playerPrefab;
    private EnemyController zakoPrefab;
    private EnemyController bossPrefab;
    private Tile walkableTilePrefab;
    private Tile nonWalkableTilePrefab;
    private static List<Vector2Int> bossCoords;
    public static Transform tileParent;

    private void Awake() {
        tileMap = new Tile[mapSize.x * phaseCount + phaseCount + 1, mapSize.y];

        tileParent = GameObject.Find("tileParentParent")?.transform.GetChild(0).transform;
        tileParent?.gameObject.SetActive(true);

        if (ReGenerate) {
            ReGenerate = false;
            PlayerController.CurrentTile = null;
            bossCoords = null;
            isTileInitialized = false;

            if (tileParent)
                DestroyImmediate(tileParent.parent.gameObject);
            tileParent = null;
        }

        if (tileParent == null) {
            tileParent = new GameObject("TileParent").transform;
            tileParent.parent = new GameObject("tileParentParent").transform;
            DontDestroyOnLoad(tileParent.parent);
        }
        else {
            for (int i = 0; i < tileParent.childCount; ++i) {
                Tile tile = tileParent.GetChild(i).GetComponent<Tile>();
                tileMap[tile.r, tile.c] = tile;
            }
        }

        playerPrefab = Resources.Load<PlayerController>("Hexagonal/PlayerCharacter");
        zakoPrefab = Resources.Load<EnemyController>("Hexagonal/ZakoEnemy");
        bossPrefab = Resources.Load<EnemyController>("Hexagonal/BossEnemy");
        walkableTilePrefab = Resources.Load<Tile>("Hexagonal/WalkableTile");
        nonWalkableTilePrefab = Resources.Load<Tile>("Hexagonal/NonWalkableTile");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            ReGenerate = true;
            SceneSwitcher.ReLoadScene();
        }
    }

    private void Start() {
        if (PlayerController.CurrentTile == null) {
            var playerCoord = InstantiatePass(Vector2Int.zero);
            PlayerController.CurrentTile = tileMap[playerCoord.x, playerCoord.y];
        }
        
        if (bossCoords == null) {
            bossCoords = InstantiateMap();
        }
        else if (bossCoords.Count > 0) {
            Vector2Int p = bossCoords[0];
            Vector3 position = CalculatePosition(bossCoords[0]);
            Destroy(tileMap[bossCoords[0].x, bossCoords[0].y].gameObject);
            bossCoords.RemoveAt(0);

            GameObject obj = GameObject.Find("Boss " + (phaseCount - bossCoords.Count).ToString());
            Destroy(obj);

            var tile = Instantiate(
                walkableTilePrefab,
                position,
                Quaternion.identity,
                tileParent
            );

            tile.name = $"Tile {p}";
            tile.r = (p).x;
            tile.c = (p).y;
            tileMap[p.x, p.y] = tile;

            PlayerController.CurrentTile = tile;
        }

        if (!isTileInitialized) {
            isTileInitialized = true;
            InitializeMap();
        }
        else {
            foreach (Tile t in tileMap) {
                t.ReInitialize(tileMap, new Vector2Int(t.r, t.c));
            }
        }

        Instantiate(playerPrefab).Initialize(PlayerController.CurrentTile);
        int i = 0;
        foreach (var bossCoord in bossCoords) {
            EnemyController boss = Instantiate(bossPrefab);
            boss.Initialize(tileMap[bossCoord.x, bossCoord.y]);
            boss.gameObject.name = "Boss " + (i++).ToString();
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
                    tileParent
                );

                tile.name = $"Tile {coord + offset}";
                tile.r = (coord + offset).x;
                tile.c = (coord + offset).y;
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
                tileParent
            );

            tile.name = $"Tile {coord + offset}";
            tile.r = (coord + offset).x;
            tile.c = (coord + offset).y;
            tileMap[coord.x + offset.x, coord.y + offset.y] = tile;
        }
        return spawnCoord + offset;
    }

    private void InitializeMap() {
        for (var coord = Vector2Int.zero; coord.y < tileMap.GetLength(1); coord.y++) {
            for (coord.x = 0; coord.x < tileMap.GetLength(0); coord.x++) {
                var tile = tileMap[coord.x, coord.y];
                tile.ReInitialize(tileMap, coord);
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