using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject explosion;
    public int damage;
    public GameObject soundObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(soundObject, transform.position, transform.rotation);
        Invoke("DestroyProjectile", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyScript>().TakeDamage(damage);
            DestroyProjectile();
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<BossScript>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
