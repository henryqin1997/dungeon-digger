using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : MonoBehaviour
{
    public int damageToGive = 1;
    public float destructTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(destructTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<EnemyControler>().DamageEnemy(damageToGive);
        }
        if (collision.gameObject.tag != "Beam")
        {
            Destroy(gameObject);
        }
    }

    public void ChangeDamage(int damageChange)
    {
        damageToGive += damageChange;
        if (damageToGive < 1)
        {
            damageToGive = 1;
        }
    }

    public void ChangeRange(int rangeChange)
    {
        destructTime += (rangeChange / 10.0f);
        if (destructTime < 0.2f)
        {
            destructTime = 0.2f;
        }
    }
}
