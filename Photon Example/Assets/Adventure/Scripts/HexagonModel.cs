using System;
using UnityEngine;

public class HexagonModel : MonoBehaviour
{
    private Hexagon Hex;

    public void SetHexagon(Hexagon hex)
    {
        Hex = hex;
    }

    public Hexagon GetHex()
    {
        return Hex;
    }
}
