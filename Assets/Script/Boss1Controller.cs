using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    public GameObject fire;
    public Transform firepos;
    Animator animator;
    private Transform target;
    public float speed;

    private float timer;
    bool isInvincible;

    public int currentHealth;
    public int maxHealth = 1;
    public EnemyHP enemyHP;
    int phase;
    private Rigidbody2D EnemyRB;

    [SerializeField] GameObject Hitbox;
    [SerializeField] GameObject area;
    [SerializeField] AreaEffector2D effector;
    public TheEnd theEnd;
    // Start is called before the first frame update
    void Start()
    {
        phase = 1;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyHP.SetHealth(currentHealth, maxHealth);
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 60)
        {
            phase = 2;
        }

        if (phase == 1)
        {


            timer += Time.deltaTime;

            if (timer > 1.5f)
            {
                animator.Play("Boss1_attack");
                if (timer > 2.65f)
                {
                    shoot();

                    timer = 0;
                    animator.Play("Boss1_walk");
                }

            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if(phase == 2)
        {
            isInvincible = true;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(11.5f, transform.position.y), speed *2* Time.deltaTime);
            if (currentHealth > 0)
            {

                if (transform.position.x == 11.5f)
                {
                    isInvincible = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                    animator.Play("Boss1_Hi2");
                    area.SetActive(true);
                    Hitbox.SetActive(true);
                }
                else
                {
                    animator.Play("Boss1_walk");
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (currentHealth <= 35)
                {
                    effector.forceMagnitude = 150;
                }
            }
            if (currentHealth <= 0)
            {
                
                area.SetActive(false); 
                animator.Play("Boss1_Death");
                Defeat();

            }
        }
    }

    public void TakeDamage(int damage)
    {
        // take damage if not invincible
        if (!isInvincible)
        {
            // take damage amount from health and call defeat if no health
            currentHealth -= damage;
            enemyHP.SetHealth(currentHealth, maxHealth);
            Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }

    void Defeat()
    {
        // remove this enemy *poof*
        Destroy(gameObject, 1.1f);
        Invoke("tamat", 0.9f); // call theEnd.tamat() after a 1-second delay
    }

    void tamat()
    {
        theEnd.tamat();
    }

    void shoot()
    {
        Instantiate(fire, firepos.position, Quaternion.identity);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // check for collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakeDamage(100);

            // Use a coroutine to delay the damage application
        }
    }
}
