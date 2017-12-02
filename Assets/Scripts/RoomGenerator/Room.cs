using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    private int _neighboursCount;

    private bool[] _openSides;
    private int _posX, _posY;
    public Room(int posX,int posY)
    {
        _posX = posX;
        _posY = posY;
        _openSides = new bool[4];
        _neighboursCount = 0;
    }

    public int NeighbourCount
    {
        get { return _neighboursCount; }
        //set { _neighboursCount = value; }
    }

    public int X
    {
        get { return _posX; }
    }

    public int Y
    {
        get { return _posY; }
    }
}
