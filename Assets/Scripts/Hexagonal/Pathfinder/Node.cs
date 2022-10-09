using System;

public class Node : IComparable<Node>, IComparable {
    public Node Parent { get; set; }
    public Tile Tile { get; set; }
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;

    public Node(Tile tile, Tile startTile, Tile targetTile) {
        Tile = tile;
        G = Tile.Distance(tile, startTile);
        H = Tile.Distance(tile, targetTile);
    }

    public int CompareTo(Node other) {
        return F == other.F ? H.CompareTo(other.H) : F.CompareTo(other.F);
    }

    int IComparable.CompareTo(object obj) {
        if (obj is not Node other) {
            throw new ArgumentException("Object is not a Node");
        }
        return CompareTo(other);
    }
}
