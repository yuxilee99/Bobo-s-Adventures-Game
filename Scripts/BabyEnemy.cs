using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BabyEnemy : MonoBehaviour
{
    public float speed;
    public bool faceLeft = true;
 
    private CharacterController2D controller;
 
    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalMove = speed * Time.fixedDeltaTime;
 
        if (faceLeft) { verticalMove *= -0.5f; }
 
        controller.Move(verticalMove, false, true);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            Debug.Log("Hit hit the Collider");
            faceLeft = !faceLeft;

            SoundManager.S.MakeEnemyHopSound();

        } else if (collision.gameObject.tag == "Player" && GameManager.S.gameState == GameState.playing)
        {
            // hit collider, destroy player
            Debug.Log("Bobo DIES!!!");

            PlayerMovement.S.PlayerDieAnimation();

            GameManager.S.PlayerDestroyed();

            Destroy(collision.gameObject);
        }
         
    }
 
}