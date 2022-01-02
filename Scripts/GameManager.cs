using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState {getReady, playing, oops, gameOver, roundWin};

public class GameManager : MonoBehaviour
{
    // Singleton Declaration
    public static GameManager S;

    // Game State
    public GameState gameState;

    // UI variables
    public TextMeshProUGUI livesMessage;
    public TextMeshProUGUI messageOverlay;
    public TextMeshProUGUI scoreMessage;
    public Button menuButton;

    // Game variables
    private int livesLeft;
    private int livesStart = 3;
    private int score;

    // player variables
    // attacker variables
    public bool collision;

    // extra life
    //public GameObject extraLife;
    private float time = 0.0f;
    private float period = 15.0f;

    private void Awake() {
        //singleton definition
        if (GameManager.S) {
            // singleton exists, delete this object
            Destroy(this.gameObject);
        } else {
            S = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        StartANewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.playing) {
            time += Time.deltaTime;
 
            // if (time >= period) {
            //     time = time - period;
            //     getLife();
            // }
        } 
        if (gameState == GameState.gameOver) {
            if (Input.GetKeyDown(KeyCode.R)) {
                Debug.Log("restart game");
                LevelManager.S.ReturnToMainMenu();
            }
        }
    }

    private void StartANewGame() {
        // reset lives
        livesLeft = livesStart;
        livesMessage.text = "LIVES: " + livesLeft;

        // reset score
        score = 0;
        scoreMessage.text = "SCORE: " + score;

        // reset round
        ResetRound();
    }

    public void ResetRound() {      
        // put game into get ready state
        gameState = GameState.getReady;

        // start the get ready coroutine
        StartCoroutine(GetReadyState());

        menuButton.gameObject.SetActive(false);
    }

    public void GameOverState() {
        // set the gamestate to game over
        gameState = GameState.gameOver;

        // display Game Over and Final Score
        messageOverlay.enabled = true;
        messageOverlay.text = "GAME OVER\n FINAL SCORE: " + score;

        // show restart button
        menuButton.gameObject.SetActive(true);
    }

    public void WinState() {
        // set the gamestate to game over
        gameState = GameState.gameOver;

        SoundManager.S.MakeWinSound();

        // display Game Over and Final Score
        messageOverlay.enabled = true;
        messageOverlay.text = "YOU WON!\n FINAL SCORE: " + score;

        // show restart button
        menuButton.gameObject.SetActive(true);
    }

    public IEnumerator LevelComplete() {
        Debug.Log("level completes");
        // set the gamestate to game over
        gameState = GameState.roundWin;

        // display Game Over and Final Score
        messageOverlay.enabled = true;
        messageOverlay.text = "YOU WIN!";

        yield return new WaitForSeconds(3.0f);

        LevelManager.S.RoundWin();

    }

    private void StartRound() {
        gameState = GameState.playing;
    }

    public IEnumerator GetReadyState() {
        // put game into get ready state
        gameState = GameState.getReady;

        // turn on message
        messageOverlay.enabled = true;
        messageOverlay.text = "" + LevelManager.S.levelName + "\n\nGET READY!";

        // pause for 2 seconds
        yield return new WaitForSeconds(2.0f);

        // turn off message
        messageOverlay.enabled = false;

        // // start music again
        // SoundManager.S.StartSounds();

        // start the game
        StartRound();
    }

    public void PlayerDestroyed() {
        // make the explosion sound
        // SoundManager.S.StopAllSounds();
        SoundManager.S.MakePlayerDieSound();

        // remove a live
        livesLeft--;
        livesMessage.text = "LIVES: " + livesLeft;

        // go to oops state
        StartCoroutine(OopsState());
    }

    public IEnumerator OopsState() {
        // set the gamestate to oops
        gameState = GameState.oops;

        // turn on message
        messageOverlay.enabled = true;
        messageOverlay.text = "Lives Left: " + livesLeft;
        livesMessage.text = "LIVES: " + livesLeft;

        // leave message for 2 seconds
        yield return new WaitForSeconds(2.0f);

        // turn off message
        messageOverlay.enabled = false;

        // check if still have lives
        if (livesLeft > 0) {
            // start the get ready coroutine
            ResetRound();
            LevelManager.S.RestartLevel();
        } else {
            GameOverState();
        }
    }

    public void UpdateScore(int points) {
        score += points;
        scoreMessage.text = "SCORE: " + score;
    }
    
    // public void getLife() {
    //     var position = new Vector3(Random.Range(-19.0f, 19.0f), 0, 0);
    //     Instantiate(extraLife, position, Quaternion.identity);
    // }

    // public void updateLife() {
    //     SoundManager.S.MakeLifeSound();

    //     livesLeft++;
    //     livesMessage.text = "LIVES: " + livesLeft;
    // }

}
