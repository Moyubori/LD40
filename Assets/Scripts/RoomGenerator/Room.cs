using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.RoomGenerator;
using Assets.Scripts.Utils;
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
                    Type = RoomType.STRAIGHT;
                    for (int i = 0; i < 4; i++)
                    {
                        if (_openSides[i] == _openSides[(i + 1)%4])
                        {
                            Type = RoomType.TURN;
                            break;
                        }
                    }
                    break;
                case 3: 
                    Type = RoomType.T;
                    break;
                case 4:
                    Type = RoomType.CROSS;
                    break;
            }
            switch (Type)
            {
                case RoomType.STRAIGHT:
                    if (_openSides[(int) Directions.DOWN]) Rotation =  90;
                    break;
                case RoomType.END:
                    for (int i = 0; i <4; i ++)
                    {
                        if (_openSides[i]) Rotation =  i * 90+90;
                        
                    }
                    
                    break;
                case RoomType.T:
                    for (int i = 0; i < 4; i++)
                    {
                        if (_openSides[i]&&_openSides[(i+2)%4]&&_openSides[(i+3)%4]) Rotation =  i * 90;
                    }
                    
                    break;
                case RoomType.TURN:
                    for (int i = 0; i < 4; i++)
                    {
                        if (_openSides[i] && _openSides[(i +1) % 4] ) Rotation = i * 90+180;
                    }
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
