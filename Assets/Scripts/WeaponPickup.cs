using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponToEquip;
    public GameObject pickupEffect;
    public GameObject soundObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
            Destroy(gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Instantiate(soundObject, transform.position, transform.rotation);
        }
    }
}
