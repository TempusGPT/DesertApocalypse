using System;
using UnityEngine;

public partial class Tile {
    private static class Initializer {
        public static void Initialize(Tile tile, Tile[,] tileMap, Vector2Int coord) {
            var xEven = coord.x % 2 == 0;
            foreach (TileDirection direction in Enum.GetValues(typeof(TileDirection))) {
                var offset = DirectionToOffset(direction, xEven);
                AddNearTile(tile, tileMap, coord, offset, direction);
            }
        }

        private static void AddNearTile(
            Tile thisTile,
            Tile[,] tileMap,
            Vector2Int coord,
            Vector2Int offset,
            TileDirection direction
        ) {
            var result = coord + offset;
            if (result.x < 0 || result.x >= tileMap.GetLength(0) ||
                result.y < 0 || result.y >= tileMap.GetLength(1)) {
                return;
            }

            var tile = tileMap[result.x, result.y];
            thisTile.nearTilesMap.Add(direction, tile);
        }

        private static Vector2Int DirectionToOffset(TileDirection direction, bool xEven) {
            return direction switch {
                TileDirection.Lower => new Vector2Int(0, -1),
                TileDirection.LowerLeft => new Vector2Int(-1, xEven ? 0 : -1),
                TileDirection.LowerRight => new Vector2Int(1, xEven ? 0 : -1),
                TileDirection.Upper => new Vector2Int(0, 1),
                TileDirection.UpperLeft => new Vector2Int(-1, xEven ? 1 : 0),
                TileDirection.UpperRight => new Vector2Int(1, xEven ? 1 : 0),
                _ => throw new ArgumentOutOfRangeException(
                    nameof(direction),
                    direction,
                    null
                )
            };
        }
    }
}