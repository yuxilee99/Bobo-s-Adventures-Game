using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !PlayerMovement.S.hit) {
            // destroy player
            Debug.Log("HIT");

            PlayerMovement.S.PlayerDieAnimation();
            
            GameManager.S.PlayerDestroyed();
        } 
    }
}
