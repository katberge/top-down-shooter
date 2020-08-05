using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : EnemyScript
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummons;
    private float summonTime;
    public GameObject enemyToSummon;

    public float attackSpeed;
    public float stopDistance;
    private float attackTime;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            if (Vector2.Distance(transform.position, targetPosition) > 0.5f) {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            } else {
                anim.SetBool("isRunning", false);

                if (Time.time >= summonTime) {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }
        }

        if (Vector2.Distance(transform.position, player.position) < stopDistance) {
                if (Time.time >= attackTime) {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
    }

    // summon minion (bird spawn)
    public void Summon()
    {
        if (player != null) {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack() 
            {
                player.GetComponent<Player>().TakeDamage(damage);
                Vector2 originalPosition = transform.position;
                Vector2 targetPosition = player.position;
                float percent = 0;
                while (percent <= 1) {
                    percent += Time.deltaTime * attackSpeed;
                    float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                    transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
                    yield return null;
                }
            }
}
