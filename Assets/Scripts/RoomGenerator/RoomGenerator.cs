using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.RoomGenerator;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;


public class RoomGenerator : MonoBehaviour
{
    private bool[,] _map;
    private List<Room> _rooms;
    [SerializeField]
    private int _dungeonSize;
    private int _posX, _posY;
    [SerializeField]
    private GameObject  _mapGO;
    [SerializeField]
    private List<GameObject> _testRoom, _crossRoom, _straightRoom, _turnRoom, _endRoom, _tRoom;
    public List<NavMeshSurface> surfaces;
	// Use this for initialization
	void Awake ()
	{
	    _map = new bool[15, 15];
	    for (int i = 0; i < _map.GetLength(0); i++)
	    {
	        for (int j = 0; j < _map.GetLength(1); j++)
	        {
	            _map[i,j] = false;
	        }
	    }
	    _map[7, 7] = true; //settining middle
	    _posX = _posY = 7;
	    _rooms = new List<Room> {new Room(7, 7)};
	    surfaces = new List<NavMeshSurface>();
        GenerateMap();
	    surfaces[0].BuildNavMesh();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateMap()
    {
        for (int i = 0; i < _dungeonSize; i++)
        {
            var choosenRoom = _rooms[Random.Range(0, _rooms.Count)];
            _posY = choosenRoom.Y;
            _posX = choosenRoom.X;
            while (!IsFreePlaceArrond())
            {
                choosenRoom = _rooms[Random.Range(0, _rooms.Count)];
                _posY = choosenRoom.Y;
                _posX = choosenRoom.X;
            }
            CreateRoom();
        }
        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                _map[i, j] = false;
                _map[i, j] = _rooms.Any(r => r.X == i && r.Y == j);
            }
        }
       
        foreach (var r in _rooms)
        {
            bool []openSides = new bool[4];
            if (r.X-1>=0&&_map[r.X - 1, r.Y]) openSides[(int)Directions.LEFT] = true;
            if( r.X + 1 < _map.GetLength(0) && _map[r.X+1,r.Y]) openSides[(int)Directions.RIGHT] = true;
            if (r.Y + 1 < _map.GetLength(0) && _map[r.X , r.Y+1]) openSides[(int)Directions.UP] = true;
            if (r.Y- 1 >= 0  && _map[r.X , r.Y-1]) openSides[(int)Directions.DOWN] = true;
            r.OpenSides = openSides;
            GameObject tileToSpawn=null;
            switch (r.Type)
            {
                case RoomType.T: tileToSpawn = _tRoom[Random.Range(0,_tRoom.Count)];
                    break;
                case RoomType.CROSS: tileToSpawn = _crossRoom[Random.Range(0,_crossRoom.Count)];
                    break;
                case RoomType.END: tileToSpawn = _endRoom[Random.Range(0,_endRoom.Count)];
                    break;
                case RoomType.STRAIGHT: tileToSpawn = _straightRoom[Random.Range(0,_straightRoom.Count)];
                    break;
                case RoomType.TURN: tileToSpawn = _turnRoom[Random.Range(0,_turnRoom.Count)];
                    break;
            }
            tileToSpawn.transform.eulerAngles = new Vector3(0,r.Rotation,0);
            var room = Instantiate(tileToSpawn, new Vector3(16*r.X, 0, 16*r.Y), tileToSpawn.transform.rotation);
            room.transform.SetParent(_mapGO.transform);
            for (int i = 0; i < room.transform.childCount; i++)
            {
                if(room.transform.GetChild(0).GetComponent<NavMeshSurface>()!=null)
                surfaces.Add(room.transform.GetChild(0).GetComponent<NavMeshSurface>());
            }

        }
    }

    bool IsFreePlaceArrond()
    {
        int neighboursCount = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == j||i==-1*j) continue;
                
                if (_posX + i == _map.GetLength(0) || _posX + i < 0 || _posY + j == _map.GetLength(1) ||
                    _posY + j < 0||_map[_posX + i, _posY + j]) neighboursCount++;
               
            }
        }
        return neighboursCount != 4;
    }

    void CreateRoom()
    {
        while (true)
        {
            var choice = Random.Range(0, 4);
            int newX = 0, newY = 0;
            switch (choice)
            {
                case (int) Directions.UP:
                    newX = _posX;
                    newY = _posY + 1;
                    break;
                case (int) Directions.DOWN:
                    newX = _posX;
                    newY = _posY - 1;
                    break;
                case (int) Directions.LEFT:
                    newX = _posX - 1;
                    newY = _posY;
                    break;
                case (int) Directions.RIGHT:
                    newX = _posX + 1;
                    newY = _posY;
                    break;
            }
            if (newX>=_map.GetLength(0)||newX<0||newY>=_map.GetLength(1)||newY<0|| _map[newX, newY])
            {
            }
            else
            {
                _map[newX, newY] = true;
                _rooms.Add(new Room(newX, newY));
                if (_rooms.Count % 3 == 0)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (newX + i >= _map.GetLength(0) || newX + i < 0 || newY + j >= _map.GetLength(1) ||
                                newY + j < 0) continue;
                            if (i == j || i == -1 * j)
                            {
                                _map[newX+i, newY+j] = true;
                            }
                        }
                    }
                    
                }
                return;
            }
        }
    }
}
