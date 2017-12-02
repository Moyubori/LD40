using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.RoomGenerator;
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

    public RoomType Type { get; set; }

    public int Rotation { get; set; }

    public int NeighbourCount
    {
        get { return _neighboursCount; }
        //set { _neighboursCount = value; }
    }

    public bool[] OpenSides
    {
        get { return _openSides; }
        set
        {
            _openSides = value;
            foreach (var s in _openSides)
            {
                if (s) _neighboursCount++;
            }
            switch (_neighboursCount)
            {
                case 1:
                    Type = RoomType.END;
                    break;
                case 2:
                    for (int i = 1; i < 4; i++)
                    {
                        if (_openSides[i] == _openSides[i - 1])
                        {
                            Type = RoomType.TURN;
                            break;
                        }
                    }
                    Type = RoomType.STRAIGHT;
                    break;
                case 3: 
                    Type = RoomType.T;
                    break;
                case 4:
                    Type = RoomType.CROSS;
                    break;
            }
        }
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
