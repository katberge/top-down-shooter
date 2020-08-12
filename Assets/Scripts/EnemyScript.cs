using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;
    public GameObject deathExplosion;

    public int pickupChance;
    public GameObject[] pickups;

    [HideInInspector]
    public Transform player;

    public virtual void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) {
            int randomNum = Random.Range(0, 101);
            if (randomNum < pickupChance) {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, randomPickup.transform.rotation);
            }
            Destroy(gameObject);
            Instantiate(deathExplosion, transform.position, transform.rotation);
        }
    }
}
