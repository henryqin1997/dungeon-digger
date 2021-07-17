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

    public UnityEvent<GameObject> roomEnteredEvent = new UnityEvent<GameObject>();

    public string roomtype = "normal";

    // Start is called before the first frame update
    void Start()
    {   
        if (roomtype.Equals("normal")){
            int enemy_count = (int) Random.Range(2,6);
            GameObject[] enemylist = Resources.LoadAll<GameObject>("Prefabs/Enemy");

            for (int i=0; i<enemy_count; i++){
                //Debug.Log("Creating enemy number: " + i);
                int index = Random.Range(0,enemylist.Length);
                GameObject enemy = Instantiate(enemylist[index]);
                enemy.SetActive(false);
                enemy.transform.position = new Vector3(transform.position.x + Random.Range(-10f,10f),transform.position.y + Random.Range(-4f,4f),12.07283f);
                enemies.Add(enemy);
                enemy.GetComponent<EnemyBody>().SetOnDestroyCallback(
                    delegate { enemies.Remove(enemy); }
                );
            }
        }


        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomEnteredEvent.Invoke(gameObject);

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
