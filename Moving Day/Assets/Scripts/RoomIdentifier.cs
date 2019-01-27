using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIdentifier : MonoBehaviour {
    
    [SerializeField] Transform[] RoomMins;
    [SerializeField] Transform[] RoomMaxs;

	public int InRoom(Vector3 position)
    {
        for(int i = 0; i < RoomMaxs.Length; i++)
        {
            if(position.x >= RoomMins[i].position.x
                && position.x < RoomMaxs[i].position.x
                && position.z >= RoomMins[i].position.z
                && position.z < RoomMaxs[i].position.z)
            {
                return i;
            }
        }
        return -1;
    }
}
