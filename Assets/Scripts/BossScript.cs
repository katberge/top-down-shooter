using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    private Transform player;
    public float attackSpeed;
    public float stopDistance;
    private float attackTime;
    public float timeBetweenAttacks;

    public int health;
    public GameObject[] enemies;
    public int spawnOffset;
    public int damage;
    public GameObject dealthMark;
    public GameObject dealthParticles;

    private int halfHealth;
    private Animator anim;
    private Slider healthBar;
    public GameObject fallSound;
    public GameObject neckSound;
    public GameObject deathSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindAnyObjectByType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                // do not attack if boss intro animation is playing
                if (Time.time >= attackTime && !anim.GetCurrentAnimatorStateInfo(0).IsName("bigShake"))
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathSound, transform.position, transform.rotation);
            Instantiate(dealthParticles, transform.position, transform.rotation);
            Instantiate(dealthMark, transform.position, transform.rotation);
            healthBar.gameObject.SetActive(false);
        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    public void IntroEffect()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("bigShake");
        Instantiate(fallSound, transform.position, transform.rotation);
    }

    public void NeckCrack()
    {
        Instantiate(neckSound, transform.position, transform.rotation);
    }
}
