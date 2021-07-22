using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_generator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject MainCamera;
    private List<GameObject> rooms = new List<GameObject>();
    public int level;

    void Start()
    {
    	level = 1;
        generate_level();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate_level()
    {
    	//Legal input level: 1, 2, 3

    	player.transform.position = new Vector3(0, 0, player.transform.position.z);
    	MainCamera.transform.position = new Vector3(0, 0, -10);

    	List<RoomConfig> layout = RoomLayoutGenerator.GenerateRoomLayout(1);

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
			int enemy_count = 0;
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
    			else //load fridge as bonus for defeat boss
    			{
    				int gift_count = Random.Range(2,4);
		            GameObject fridge = Resources.Load<GameObject>("Prefabs/Fridge");
		            for (int i=0; i<gift_count; i++)
		            {
		                GameObject fridge_ = Instantiate(fridge);
		                fridge_.transform.position = new Vector3(transform.position.x + Random.Range(-2f,2f), transform.position.y + Random.Range(-1f,1f),12.07283f);
		                fridge_.transform.parent = this.transform;
		                fridge_.SetActive(true);
		            }
    			}
    		}
    		else if (rc.type == "boss")
    		{
    			temproom.GetComponent<Room>().closeWhenEntered = true;
    			temproom.GetComponent<Room>().openWhenEnemiesCleared = true;
    			GameObject boss;
    			if (level == 1) {
    				//boss = Resources.Load("Prefabs/Boss/Boss System") as GameObject;
    				boss = Resources.Load("Prefabs/Boss/Boss Nezha System") as GameObject;
    			}
    			else if (level == 2){
    				boss = Resources.Load("Prefabs/Boss2/Boss2 System") as GameObject;
    			}
    			else{
    				boss = Resources.Load("Prefabs/Boss/Boss Nezha System") as GameObject;
    			}
    			boss = Instantiate(boss);
    			boss.transform.position = new Vector3(28.8f*rc.position.x, 16f*rc.position.y, 12.07283f);
    			boss.transform.parent = temproom.transform;
    			temproom.GetComponent<Room>().enemies.Add(boss);
    		}
    		else if (rc.type == "treasure")
    		{}
    		else if (rc.type == "challenge")
    		{
    			temproom.GetComponent<Room>().closeWhenEntered = true;
    			temproom.GetComponent<Room>().openWhenEnemiesCleared = true;
    			enemy_count = 7 + level;
    		}
    		else
    		{
    			enemy_count = 5 + level;
    			temproom.GetComponent<Room>().closeWhenEntered = true;
    			temproom.GetComponent<Room>().openWhenEnemiesCleared = true;
    		}
    		Room tmp = temproom.GetComponent<Room>();
    		tmp.setup(enemy_count);
    	}
    	level += 1;
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
