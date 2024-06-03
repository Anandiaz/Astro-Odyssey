using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss1Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // check for collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("DemoBoss1");

        }
    }
}
