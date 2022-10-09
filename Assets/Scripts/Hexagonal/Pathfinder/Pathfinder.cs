using System.Collections.Generic;
using System.Linq;

public class Pathfinder {
    private readonly Dictionary<Tile, Node> nodeMap = new();
    private readonly SortedSet<Node> openSet = new();
    private readonly HashSet<Node> closedSet = new();

    public IEnumerable<Tile> Find(Tile startTile, Tile targetTile) {
        if (startTile == targetTile) {
            return Nothing();
        }
        var (startNode, targetNode) = InitializeNode(startTile, targetTile);

        while (openSet.Count > 0) {
            var currentNode = GetCurrentNode();
            if (currentNode == targetNode) {
                return RetracePath(currentNode, startNode).Reverse();
            }

            foreach (
                var nearTile
                in currentNode.Tile.NearTiles.Values.Where(tile => tile.IsWalkable)
            ) {
                if (!nodeMap.TryGetValue(nearTile, out var nearNode)) {
                    nearNode = new Node(nearTile, startTile, targetTile);
                    nodeMap.Add(nearTile, nearNode);
                }
                UpdateNearNode(currentNode, nearNode);
            }
        }

        return Nothing();
    }

    private (Node startNode, Node targetNode) InitializeNode(Tile startTile, Tile targetTile) {
        nodeMap.Clear();
        openSet.Clear();
        closedSet.Clear();

        var startNode = new Node(startTile, startTile, targetTile);
        var targetNode = new Node(targetTile, startTile, targetTile);

        nodeMap.Add(startTile, startNode);
        nodeMap.Add(targetTile, targetNode);
        openSet.Add(startNode);
        return (startNode, targetNode);
    }

    private Node GetCurrentNode() {
        var currentNode = openSet.Min;
        openSet.Remove(currentNode);
        closedSet.Add(currentNode);
        return currentNode;
    }

    private void UpdateNearNode(Node currentNode, Node nearNode) {
        if (closedSet.Contains(nearNode)) {
            return;
        }

        var tentativeG = currentNode.G + Tile.Distance(currentNode.Tile, nearNode.Tile);
        if (tentativeG >= nearNode.G && openSet.Contains(nearNode)) {
            return;
        }

        nearNode.G = tentativeG;
        nearNode.Parent = currentNode;
        if (!openSet.Contains(nearNode)) {
            openSet.Add(nearNode);
        }
    }

    private static IEnumerable<Tile> RetracePath(Node currentNode, Node startNode) {
        while (currentNode != startNode) {
            yield return currentNode.Tile;
            currentNode = currentNode.Parent;
        }
    }

    private static IEnumerable<Tile> Nothing() {
        yield break;
    }
}
