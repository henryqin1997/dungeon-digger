using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered, openWhenEnemiesCleared;

    public GameObject[] doors;

    public List<GameObject> enemies = new List<GameObject>();

    private bool roomActive;

    // public UnityEvent<GameObject> roomEnteredEvent = new UnityEvent<GameObject>();

    public string roomtype = "normal";

    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roomActive && closeWhenEntered && (enemies.Count == 0))
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
            closeWhenEntered = false;
        }
        if (roomtype.Equals("challenge") && enemies.Count == 0) //load random prefab
        {
            int gift_count = Random.Range(1,3);
            GameObject fridge = Resources.Load<GameObject>("Prefabs/Fridge");
            for (int i=0; i<gift_count; i++)
            {
                GameObject fridge_ = Instantiate(fridge);
                fridge_.transform.position = new Vector3(transform.position.x - gift_count + 1 + 2f*i, transform.position.y ,12.07283f);
                fridge_.transform.parent = this.transform;
                fridge_.SetActive(true);
            }
            roomtype = "challenge rewarded";
        }
        if (roomtype.Equals("normal") && enemies.Count == 0) //load random prefab
        {
            int chance = Random.Range(1,5);
            if (chance>=4){
                GameObject fridge = Resources.Load<GameObject>("Prefabs/Fridge");
                GameObject fridge_ = Instantiate(fridge);
                fridge_.transform.position = new Vector3(transform.position.x, transform.position.y, 12.07283f);
                fridge_.transform.parent = this.transform;
                fridge_.SetActive(true);
            }
            roomtype = "normal rewarded";
        }
    }

    public void setup(int enemy_num)
    {
        if (roomtype.Equals("normal") || roomtype.Equals("challenge")){
            int enemy_count = (int) Random.Range(enemy_num-4,enemy_num);
            GameObject[] enemylist = Resources.LoadAll<GameObject>("Prefabs/Enemy");

            for (int i=0; i<enemy_count; i++){
                //Debug.Log("Creating enemy number: " + i);
                int index = Random.Range(0,enemylist.Length);
                GameObject enemy = Instantiate(enemylist[index]);
                enemy.SetActive(false);
                enemy.transform.position = new Vector3(transform.position.x + Random.Range(-10f,10f),transform.position.y + Random.Range(-4f,4f),12.07283f);
                enemies.Add(enemy);
                enemy.transform.parent = this.transform;
                enemy.GetComponent<EnemyBody>().SetOnDestroyCallback(
                    delegate { enemies.Remove(enemy); }
                );
            }
        }

        if (roomtype.Equals("treasure"))
        {
            int gift_count = Random.Range(1,3);
            GameObject fridge = Resources.Load<GameObject>("Prefabs/Fridge");
            for (int i=0; i<gift_count; i++)
            {
                GameObject fridge_ = Instantiate(fridge);
                fridge_.transform.position = new Vector3(transform.position.x - gift_count + 1 + 2f*i, transform.position.y,12.07283f);
                fridge_.transform.parent = this.transform;
                fridge_.SetActive(true);
            }
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // roomEnteredEvent.Invoke(gameObject);
            GameObject MainCamera = GameObject.Find("Main Camera");
            CameraController cc = MainCamera.GetComponent<CameraController>();
            cc.ChangeTarget(this.transform);

            if (closeWhenEntered)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            roomActive = true;

            Invoke("invoke_all", 0.25f);
        }
    }

    void invoke_all()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
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
