using System;

public class Node : IComparable<Node>, IComparable {
    public Node Parent { get; set; }
    public Tile Tile { get; set; }
    public float G { get; set; }
    public float H { get; set; }
    public float F => G + H;

    public Node(Tile tile, Tile startTile, Tile targetTile) {
        Tile = tile;
        G = Tile.Distance(tile, startTile);
        H = Tile.Distance(tile, targetTile);
    }

    public int CompareTo(Node other) {
        return F.CompareTo(other.F);
        // return comparedF == 0 ? H.CompareTo(other.H) : comparedF;
    }

    int IComparable.CompareTo(object obj) {
        if (obj is not Node other) {
            throw new ArgumentException("Object is not a Node");
        }
        return CompareTo(other);
    }
}