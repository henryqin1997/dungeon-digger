using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NezhaBossController : BossController
{
    public bool isAttacking = false;
    public override void Update()
    {
        base.Update();
        // change nezha body angle
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            return;
        }

        Vector3 playerPosition = player.transform.position;
        Vector3 nezhaPosition = transform.position;


        if (playerPosition.x > nezhaPosition.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }
        else
        {
            transform.localScale = Vector3.one;

        }
        if(actions[currentAction].shouldChasePlayer) {
            anim.SetBool("isAttacking", true);
            isAttacking = true;
  
        } else {
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }
}
