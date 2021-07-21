using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_generator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject MainCamera;
    private List<GameObject> rooms = new List<GameObject>();

    void Start()
    {
        generate_level(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate_level(int level)
    {
    	//Legal input level: 1, 2, 3

    	player.transform.position = new Vector3(0, 0, player.transform.position.z);
    	MainCamera.transform.position = new Vector3(0, 0, -10);

    	List<RoomConfig> layout = RoomLayoutGenerator.GenerateRoomLayout(5);

    	foreach (RoomConfig rc in layout)
    	{
    		string room_postfix = "";
    		if (((int)rc.doors & 1)>0){
    			room_postfix += "l";
    		}
    		if (((int)rc.doors & 2)>0){
    			room_postfix += "r";
    		}
    		if (((int)rc.doors & 4)>0){
    			room_postfix += "u";
    		}
    		if (((int)rc.doors & 8)>0){
    			room_postfix += "d";
    		}
    		GameObject temproom = Resources.Load("Prefabs/Room/DoorRooms/Room_"+room_postfix) as GameObject;
    		temproom = Instantiate(temproom);
    		rooms.Add(temproom);
    		temproom.transform.position = new Vector3(28.8f*rc.position.x, 16f*rc.position.y, 12.07283f);
			temproom.GetComponent<Room>().roomtype = rc.type;
    		if (rc.type == "initial")
    		{
    			temproom.GetComponent<Room>().closeWhenEntered = false;
    			temproom.GetComponent<Room>().openWhenEnemiesCleared = true;
    			
    			if (level == 1){
    				GameObject initial = Resources.Load("Prefabs/Room/Intro Display") as GameObject;
    				initial = Instantiate(initial) as GameObject;
    				initial.transform.position = new Vector3(-0.4f,1.96f,0f);
    				initial.transform.parent = temproom.transform;
    			}
    		}
    		else if (rc.type == "boss")
    		{
    			temproom.GetComponent<Room>().closeWhenEntered = true;
    			temproom.GetComponent<Room>().openWhenEnemiesCleared = true;
    			GameObject boss = Resources.Load("Prefabs/boss_prefab/Boss System") as GameObject;
    			boss = Instantiate(boss);
    			boss.transform.position = new Vector3(28.8f*rc.position.x, 16f*rc.position.y, 12.07283f);
    			boss.transform.parent = temproom.transform;
    			temproom.GetComponent<Room>().enemies.Add(boss);
    		}
    		else
    		{
    			temproom.GetComponent<Room>().closeWhenEntered = true;
    			temproom.GetComponent<Room>().openWhenEnemiesCleared = true;
    		}
    		Room tmp = temproom.GetComponent<Room>();
    		tmp.setup();
    	}

    }

    public void destroy_level()
    {
    	foreach (GameObject room in rooms)
	    {
	       Destroy(room);  
	    }
	    rooms = new List<GameObject>();
    }
}
