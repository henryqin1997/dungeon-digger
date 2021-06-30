using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : MonoBehaviour
{
    private static bool entered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
    	if (other.tag == "Player"){
    		CameraController.instance.ChangeTarget(transform);
            RoomsEnteredTracker.rooms_entered++;
            if (!entered) {
                RoomsEnteredTracker.unique_rooms_entered++;
            }
            entered = true;
    	}
    }
}