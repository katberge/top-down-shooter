using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health;
    public GameObject[] enemies;
    public int spawnOffset;
    public int damage;

    private int halfHealth;
    private Animator anim;

    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) {
            Destroy(gameObject);
        }

        if (health <= halfHealth) {
            anim.SetTrigger("stage2");
        }

        GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
