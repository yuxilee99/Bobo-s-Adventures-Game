using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BonchEnemy : MonoBehaviour
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
        float horizontalMove = speed * Time.fixedDeltaTime;
 
        if (faceLeft) { horizontalMove *= -1.0f; }
        controller.Move(horizontalMove, false, false);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            faceLeft = !faceLeft;
        } else if (collision.gameObject.tag == "Player" && GameManager.S.gameState == GameState.playing)
        {
            Debug.Log("Goodbye Cruel World!!!");

            Destroy(this.gameObject, 0.0f);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;

            SoundManager.S.MakeBonchHitSound();

            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Dying");
            speed = 0;

            GameManager.S.UpdateScore(50);
        }
         
    }

    IEnumerator StunPlayer() {
        PlayerMovement.S.hit = true;
        yield return new WaitForSeconds(1.0f);
        PlayerMovement.S.hit = false;
    }
 
}