using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Beam")
        {
            Destroy(gameObject);
        }
    }
}
