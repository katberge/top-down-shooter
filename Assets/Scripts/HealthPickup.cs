using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    public GameObject pickupEffect;
    public GameObject soundObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<Player>().Heal(healAmount);
            Destroy(gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Instantiate(soundObject, transform.position, transform.rotation);
        }
    }
}
