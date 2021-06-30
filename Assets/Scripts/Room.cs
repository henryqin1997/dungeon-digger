using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private static bool room_entered = false;
    private static bool room1_entered = false;
    private static bool room2_entered = false;
    private static bool room3_entered = false;
    private static bool room4_entered = false;
    private static bool room5_entered = false;
    private static bool room6_entered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // rooms entered tracker
    private void OnTriggerEnter2D(Collider2D other){
    	if (other.tag == "Player"){
    		CameraController.instance.ChangeTarget(transform);
            RoomsEnteredTracker.rooms_entered++;
            if (gameObject.name == "Room") {
                if (!room_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room_entered = true;
            }
            if (gameObject.name == "Room (1)") {
                if (!room1_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room1_entered = true;
            }
            if (gameObject.name == "Room (2)") {
                if (!room2_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room2_entered = true;
            }
            if (gameObject.name == "Room (3)") {
                if (!room3_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room3_entered = true;
            }
            if (gameObject.name == "Room (4)") {
                if (!room4_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room4_entered = true;
            }
            if (gameObject.name == "Room (5)") {
                if (!room5_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room5_entered = true;
            }
            if (gameObject.name == "Room (6)") {
                if (!room6_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room6_entered = true;
            }
    	}
    }
}
