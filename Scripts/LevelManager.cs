using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager S;

    [Header ("Level Info")]
    public string levelName; // string to display at the level start
    // public GameObject motherShipObject; 

    [Header ("Scene Info")]
    public string nextScene; // string that is the level name
    public bool finalScene; // indicates end of the game

    private void Awake() {
        S = this; // singleton assignment
    }

    private void Start() {
        if (GameManager.S) {
            GameManager.S.ResetRound();
        }
    }

    public void RoundWin() {
        SceneManager.LoadScene(nextScene);

    }

    public void RestartLevel() {
        // reload this scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu() {
        Debug.Log("back to main");
        SceneManager.LoadScene("TitleMenu");

        Destroy(GameManager.S.gameObject);
    }
}
