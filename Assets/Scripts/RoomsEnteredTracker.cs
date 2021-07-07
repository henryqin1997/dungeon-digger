using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class RoomsEnteredTracker : MonoBehaviour
{
    public int rooms_entered = 0;
    private HashSet<string> names_of_rooms_entered = new HashSet<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("rooms" + rooms_entered);
        //Debug.Log("unique" + UniqueRoomsEntered());
    }

    public void OnRoomEnter(GameObject room)
    {
        Debug.Assert(room != null);
        Debug.Log("Room Entered: \"" + room.name + "\"");
        names_of_rooms_entered.Add(room.name);
        ++rooms_entered;
    }

    public void OnGameOver()
    {
        Analytics.CustomEvent("roomsEntered", new Dictionary<string, object>
        {
            { "totalRoomsEntered", rooms_entered },
            { "uniqueRoomsEntered", UniqueRoomsEntered() }
        });
    }

    private int UniqueRoomsEntered()
    {
        return names_of_rooms_entered.Count;
    }
}
