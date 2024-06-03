using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool isInvincible;
    
    public int currentHealth;
    public int maxHealth = 1;
    public int contactDamage = 1;

    public float speed;
    public float circleradius;
    private Rigidbody2D EnemyRB;
    public GameObject groundcheck;
    public LayerMask groundlayer;
    public GameObject playercheck;
    public LayerMask playerlayer;
    public GameObject blockCheck;
    public bool faceright;
    public bool isGrounded;
    public bool isBlocked;
    public bool isAttack;
    bool cooling;

    public float cooldown;
    float lastAttack;
    private float intCool;

    Animator animator;

    public EnemyHP enemyHP;


    // Start is called before the first frame update
    void Start()
    {
        intCool = cooldown;
        // start at full health
        currentHealth = maxHealth;
        EnemyRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyHP.SetHealth(currentHealth,maxHealth);

        animator.SetBool("Attack",false);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.transform.position, circleradius, groundlayer);
        isBlocked = Physics2D.OverlapCircle(blockCheck.transform.position, circleradius, groundlayer);
        isAttack = Physics2D.OverlapCircle(playercheck.transform.position, circleradius, playerlayer);
        if (isAttack)
        {
            EnemyRB.velocity = Vector2.right * 0 * speed;

        }
        else
        {
            EnemyRB.velocity = Vector2.right * speed;
        }

        if (isBlocked)
        {
            Flip();
        }


        if (!isGrounded && faceright && !isAttack)
        {
            animator.Play("cobraAnimation");
            Flip();
        }
        else if(!isGrounded && !faceright && !isAttack) 
        {
            animator.Play("cobraAnimation");
            Flip();
        }

    }

    void Flip()
    {
        faceright = !faceright;
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundcheck.transform.position, circleradius);
        Gizmos.DrawWireSphere(playercheck.transform.position, circleradius);
    }

    public void Invincible(bool invincibility)
    {
        isInvincible = invincibility;
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
            if (currentHealth <= 0)
            {
                EnemyRB.velocity = Vector2.right * 0;
                Defeat();
            }
        }
    }

    void Defeat()
    {
        // remove this enemy *poof*
        animator.Play("Cobra_Died");
        Destroy(gameObject,0.9f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // check for collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            if (Time.time - lastAttack < cooldown)
            {
                animator.Play("Cobra_Hit");
                return;
            }
            lastAttack = Time.time;
            animator.Play("cobraAnimation");

            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakeDamage(this.contactDamage);

            // Use a coroutine to delay the damage application
        }
    }

}