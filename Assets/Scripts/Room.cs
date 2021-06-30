using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered, openWhenEnemiesCleared;

    public GameObject[] doors;

    public List<GameObject> enemies = new List<GameObject>();

    private bool roomActive;

    private static bool room_entered = false;
    private static bool room1_entered = false;
    private static bool room2_entered = false;
    private static bool room3_entered = false;
    private static bool room4_entered = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if (enemies.Count == 0)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);

                    closeWhenEntered = false;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            RoomsEnteredTracker.rooms_entered++;
            if (gameObject.name == "Room") {
                if (!room_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room_entered = true;
            }
            else if (gameObject.name == "Room (1)") {
                if (!room1_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room1_entered = true;
            }
            else if (gameObject.name == "Room (2)") {
                if (!room2_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room2_entered = true;
            }
            else if (gameObject.name == "Room (3)") {
                if (!room3_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room3_entered = true;
            }
            else if (gameObject.name == "Room (4)") {
                if (!room4_entered) {
                    RoomsEnteredTracker.unique_rooms_entered++;
                }
                room4_entered = true;
            }

            if (closeWhenEntered)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            roomActive = true;

            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
        }
    }

}
