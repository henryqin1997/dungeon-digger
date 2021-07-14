using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
	public Transform player;
	void LateUpdate(){
		if(player != null){
			Vector3 newPosition = player.position;
			newPosition.z = transform.position.z;
			transform.position = newPosition;
			Debug.Log("transform camera location is :" + transform.position.z);
		}
	
	}
}
