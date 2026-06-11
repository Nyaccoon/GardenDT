using UnityEngine;

public static class Support
{
    public const int GridWidth = 10;
    public const int GridHeight = 10;

    public enum FloorType
    {
        Grass,
        Flower,
        Bush,
        Tree,
        Tile,
        LeakThroughTiles,
        Gravel,
        Pond,
        Dirt
    }

    public enum PreviewState
    {
        Positive,
        Negative
    }
}
