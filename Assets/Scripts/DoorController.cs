using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool enemyLock = false;
    int roomX;
    int roomY;

    private void Update()
    {
        if(enemyLock && !enemiesLeft(roomX, roomY))
        {

        }
    }



    //return if any enemies still exist in room
    //(enemies must be child of room)
    public bool enemiesLeft(int roomX, int roomY)
    {
        string r = "room (" + roomX + "," + roomY + ")";
        GameObject room = GameObject.Find(r);

        Transform[] ts = GetComponentsInChildren<Transform>();

        foreach (Transform t in ts)
        {
            if (t.tag == "Enemy")
            {
                return true;
            }
        }

        return false;

    }


}
