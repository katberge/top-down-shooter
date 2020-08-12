using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;
    private Animator cameraAnim;

    private void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // forward is the x axis
        transform.rotation = rotation;

        if (Input.GetMouseButton(0) && Time.time >= shotTime) {
            cameraAnim.SetTrigger("smallShake");
            Instantiate(projectile, shotPoint.position, transform.rotation);
            shotTime = Time.time + timeBetweenShots;
        }
    }
}
