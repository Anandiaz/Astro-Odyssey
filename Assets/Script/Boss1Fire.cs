using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boss1Fire : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D RB;
    public float force;
    Animator animator;
    public int contactDamage = 1;
    Vector3 direction;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
            animator = GetComponent<Animator>();
            RB = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");

            direction = player.transform.position - transform.position;
            RB.velocity = new Vector2(direction.x, direction.y).normalized * force;

            float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            RB.velocity = new Vector2(direction.x, direction.y).normalized * 0;
            animator.Play("Fire_explode");
            Destroy(gameObject, 0.25f);
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // check for collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            RB.velocity = new Vector2(direction.x, direction.y).normalized * 0;
            animator.Play("Fire_explode");
            Destroy(gameObject, 0.25f);
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakeDamage(this.contactDamage);
            // colliding with player inflicts damage and takes contact damage away from health

        }
    }
}
