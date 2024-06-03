using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public int contactDamage = 1;
    private void OnTriggerStay2D(Collider2D other)
    {
        // check for collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.HitSide(transform.position.x > player.transform.position.x);
            player.TakeDamage(this.contactDamage);
            // colliding with player inflicts damage and takes contact damage away from health

        }
    }
}
