using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
 
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement S;

    public CharacterController2D controller;
    public float speed;
 
    private float horizontalMove = 0.0f;
    private bool jump = false;

    private Animator animator;

    public bool stun = false;
    public bool hit = false;

    public bool playerDead = false;


    private AudioSource audio;
 
    private void Awake() {
        S = this; // singleton assigned
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>(); // fetch the audio source component
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.gameState == GameState.playing) {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isOnGround", false);
                SoundManager.S.MakePlayerHopSound();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                StopPlayerWalkSound();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                StopPlayerWalkSound();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow)) {
                MakePlayerWalkSound();
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                MakePlayerWalkSound();
            }

            // get approximate direction
            float ourSpeed = Input.GetAxis("Horizontal");
            animator.SetFloat("speed", Mathf.Abs(ourSpeed));
        } else {
            MakePlayerWalkSound();
            animator.SetFloat("speed", 0);
        }
 
    }
 
    private void FixedUpdate()
    {
        if (GameManager.S.gameState == GameState.playing) {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        } else {
            controller.Move(0, false, false);
        }
    }

    private void MakePlayerWalkSound() {
        audio.Stop();
    }

    private void StopPlayerWalkSound() {
        audio.Play();
    }

    public void PlayerLanded() {
        animator.SetBool("isOnGround", true);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            // player has died
            PlayerDieAnimation();

            Debug.Log("OH NO BYE BOBO");

            GameManager.S.PlayerDestroyed();

            Destroy(collision.gameObject);
        } 
    }

    public void PlayerDieAnimation() {
        GetComponent<Rigidbody2D>().isKinematic = true;
        animator.SetTrigger("Death");

    }
 
}