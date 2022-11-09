using System.Collections.Generic;
using System.Linq;

public class Pathfinder {
    public IEnumerable<Tile> Find(Tile start, Tile goal) {
        var openSet = new HashSet<Tile> {
            start
        };

        var cameFrom = new Dictionary<Tile, Tile>();

        var gScore = new Dictionary<Tile, float> {
            [start] = 0
        };

        var fScore = new Dictionary<Tile, float> {
            [start] = Tile.Distance(start, goal)
        };

        while (openSet.Count > 0) {
            var current = openSet.Aggregate(
                (a, b) => GetScore(fScore, a) < GetScore(fScore, b) ? a : b
            );
            if (current == goal) {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            foreach (var neighbor in current.NearTiles.Values) {
                var tentativeG = GetScore(gScore, current)
                    + Tile.Distance(current, neighbor);
                if (tentativeG < GetScore(gScore, neighbor)) {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Tile.Distance(neighbor, goal);

                    if (!openSet.Contains(neighbor)) {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        
        return Nothing();
    }

    private static float GetScore(IReadOnlyDictionary<Tile, float> score, Tile tile) {
        return score.TryGetValue(tile, out var result)
            ? result
            : float.PositiveInfinity;
    }

    private static IEnumerable<Tile> ReconstructPath(
        Dictionary<Tile, Tile> cameFrom,
        Tile current
    ) {
        var totalPath = new List<Tile> {
            current
        };
        while (cameFrom.Keys.Contains(current)) {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }
        return totalPath.Skip(1);
    }

    private static IEnumerable<Tile> Nothing() {
        yield break;
    }
}