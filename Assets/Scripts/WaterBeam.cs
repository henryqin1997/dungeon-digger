using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : MonoBehaviour
{
    public int damageToGive = 1;
    public float destructTime = 1.5f;
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
        if (collision.gameObject.tag != "Beam")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyBody>().DamageEnemy(damageToGive);
            }
            else if (collision.gameObject.tag == "Boss")
            {
                collision.gameObject.GetComponent<BossController>().TakeDamage(damageToGive);
            }
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
    }
}
