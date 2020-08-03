using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyScript
{
    public float stopDistance;
    private string direction = "Forward";

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        float xDist = player.position.x - transform.position.x;

        if (player != null && distance > stopDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if (xDist < 0 && direction == "Forward") {
            direction = "Backward";
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (xDist >= 0 && direction == "Backward") {
            direction = "Forward";
            transform.rotation = Quaternion.identity;
        }
    }
}
