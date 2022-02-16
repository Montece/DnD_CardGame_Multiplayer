using System.Text;
using UnityEngine;

[System.Serializable]
public struct Coordinates
{
    [SerializeField]
    public int X;
    [SerializeField]
    public int Y;

    public static readonly Coordinates WrongPosition = new Coordinates(-1, -1);

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Coordinates operator +(Coordinates a, Coordinates b)
    {
        return new Coordinates(a.X + b.X, a.Y + b.Y);
    }

    public static Coordinates operator -(Coordinates a, Coordinates b)
    {
        return new Coordinates(a.X - b.X, a.Y - b.Y);
    }

    public static bool operator ==(Coordinates a, Coordinates b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Coordinates a, Coordinates b)
    {
        return a.X != b.X || a.Y != b.Y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (Coordinates)obj;
    }

    public override string ToString()
    {
        return $"X:{X} Y:{Y}";
    }
}