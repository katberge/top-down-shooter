using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public int health;
    public GameObject[] enemies;
    public int spawnOffset;
    public int damage;
    public GameObject dealthMark;
    public GameObject dealthParticles;

    private int halfHealth;
    private Animator anim;
    private Slider healthBar;

    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0) {
            Destroy(gameObject);
            Instantiate(dealthParticles, transform.position, transform.rotation);
            Instantiate(dealthMark, transform.position, transform.rotation);
            healthBar.gameObject.SetActive(false);
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

    public void IntroEffect()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("bigShake");
    }
}
