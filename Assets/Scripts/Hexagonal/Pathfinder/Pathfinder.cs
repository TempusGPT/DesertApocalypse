using System.Collections.Generic;
using System.Linq;

public class Pathfinder {
    private readonly HashSet<Tile> openSet = new();
    private readonly Dictionary<Tile, Tile> cameFrom = new();
    private readonly Dictionary<Tile, float> gScore = new();
    private readonly Dictionary<Tile, float> fScore = new();

    public IEnumerable<Tile> Find(Tile start, Tile goal) {
        openSet.Clear();
        openSet.Add(start);
        cameFrom.Clear();
        gScore.Clear();
        gScore[start] = 0;
        fScore.Clear();
        fScore[start] = Tile.Distance(start, goal);

        while (openSet.Count > 0) {
            var current = openSet.Aggregate(
                (a, b) => GetFScore(a) < GetFScore(b) ? a : b
            );
            if (current == goal) {
                return ReconstructPath(cameFrom, current).Reverse();
            }

            openSet.Remove(current);
            foreach (var near in current.NearTiles.Values) {
                var tentativeG = GetGScore(current) + Tile.Distance(current, near);
                if (tentativeG < GetGScore(near)) {
                    cameFrom[near] = current;
                    gScore[near] = tentativeG;
                    fScore[near] = tentativeG + Tile.Distance(near, goal);

                    if (!openSet.Contains(near)) {
                        openSet.Add(near);
                    }
                }
            }
        }

        return Nothing();
    }

    private float GetGScore(Tile tile) {
        return gScore.TryGetValue(tile, out var result)
            ? result
            : float.PositiveInfinity;
    }

    private float GetFScore(Tile tile) {
        return fScore.TryGetValue(tile, out var result)
            ? result
            : float.PositiveInfinity;
    }

    private static IEnumerable<Tile> ReconstructPath(
        Dictionary<Tile, Tile> cameFrom,
        Tile current
    ) {
        while (cameFrom.Keys.Contains(current)) {
            yield return current;
            current = cameFrom[current];
        }
    }

    private static IEnumerable<Tile> Nothing() {
        yield break;
    }
}