using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyScript
{
    public float stopDistance;
    private float attackTime;
    private Animator anim;
    public Transform shotPoint;
    public GameObject bugAttack;
    private string direction = "Forward";

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    void Update() 
    {
        if (player != null) {
            if (Vector2.Distance(transform.position, player.position) > stopDistance) {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } 
            if (Time.time >= attackTime) {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }

            // rotate range enemy to face spider
            float xDist = player.position.x - transform.position.x;
            if (xDist < 0 && direction == "Forward") {
                direction = "Backward";
                transform.rotation = Quaternion.Euler(0, 180, 0);
            } else if (xDist >= 0 && direction == "Backward") {
                direction = "Forward";
                transform.rotation = Quaternion.identity;
            }
        }
    }

    public void RangeAttack()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // forward is the x axis
        shotPoint.rotation = rotation;
        Instantiate(bugAttack, shotPoint.position, shotPoint.rotation); 
    }
}
