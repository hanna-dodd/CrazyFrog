using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameManager gameManager;

    public void GoHome() {

        SceneManager.LoadScene("MainMenu");

    }

    public void RestartGame() {

        Debug.Log("restart");

    }

}
