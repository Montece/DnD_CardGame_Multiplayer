using UnityEngine;

[System.Serializable]
public class PathNode
{
    [SerializeField]
    public Hexagon Hex;
    [SerializeField]
    public int PathLengthFromStart;
    [SerializeField]
    public PathNode CameFrom;
    [SerializeField]
    public int MaybePathLength;

    public int GetFullPathLength()
    {
        return PathLengthFromStart + MaybePathLength;
    }
}