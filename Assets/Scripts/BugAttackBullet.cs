using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAttackBullet : MonoBehaviour
{
    private Player playerScript;
    private Vector2 targetPosition;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > 0.1f) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
