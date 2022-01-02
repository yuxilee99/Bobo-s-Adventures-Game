using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Singleton Declaration
    public static ButtonManager S;

    private void Awake() {
            S = this;
    }

    public void btn_StartTheGame() {
        SceneManager.LoadScene("Level01 3");
    }

    public void btn_QuitGame() {
        Debug.Log("QUITING");
        Application.Quit();
    }

    public void btn_ReturnToMenu() {
        SceneManager.LoadScene("TitleMenu");
    }

    public void btn_GoToInstructions() {
        SceneManager.LoadScene("InstructionMenu");
    }

    public void btn_GoToCredits() {
        SceneManager.LoadScene("CreditsMenu");
    }
    
}
