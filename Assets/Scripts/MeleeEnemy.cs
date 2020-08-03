using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyScript
{
    public float stopDistance;
    private string direction = "Forward";
    private float attackTime;
    public float attackSpeed;

    private void Update()
    {
        if (player != null) {
            float distance = Vector2.Distance(transform.position, player.position);
            float xDist = player.position.x - transform.position.x;

            if (distance > stopDistance) {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } else {
                if (Time.time >= attackTime) {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }

            IEnumerator Attack() 
            {
                player.GetComponent<Player>().TakeDamage(damage);
                Vector2 originalPosition = transform.position;
                Vector2 targetPosition = player.position;
                float percent = 0;
                while (percent <= 1) {
                    percent += Time.deltaTime * attackSpeed;
                    float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                    transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
                    yield return null;
                }
            }

            // rotate melee enemy to face spider
            if (xDist < 0 && direction == "Forward") {
                direction = "Backward";
                transform.rotation = Quaternion.Euler(0, 180, 0);
            } else if (xDist >= 0 && direction == "Backward") {
                direction = "Forward";
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
