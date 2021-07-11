using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
	public float moveSpeed = 30;

	public Transform target;

    public void OnRoomEnter(GameObject room)
    {
        ChangeTarget(room.transform);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }

    public void ChangeTarget(Transform newTarget){
        target = newTarget;
    }
}
