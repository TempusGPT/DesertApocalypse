using System.Collections.Generic;

public static class Pathfinder {
    // open list and closed list
    private static List<Tile> openList = new();
    private static List<Tile> closedList = new();

    public static IEnumerable<Tile> Find(Tile startTile, Tile targetTile) {
        yield return targetTile;
    }
}
