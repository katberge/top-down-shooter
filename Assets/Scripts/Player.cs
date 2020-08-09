using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveAmount;
    public int health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        
        // set isRunning bool to transition between idle and running animations
        if (moveInput != Vector2.zero) { 
            anim.SetBool("isRunning", true);
        } 
        else {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void ChangeWeapon(GameObject newWeapon)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(newWeapon, new Vector2(transform.position.x + 5, transform.position.y - 1), transform.rotation, transform);
    }
}
