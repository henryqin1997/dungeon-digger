using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class RoomsEnteredTracker : MonoBehaviour
{
    public static int rooms_entered = 0;
    public static int unique_rooms_entered = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SendRoomsEntered()
    {
        Analytics.CustomEvent("roomsEntered", new Dictionary<string, object>
        {
            { "totalRoomsEntered", rooms_entered },
            { "uniqueRoomsEntered", unique_rooms_entered }
        });
    }
}