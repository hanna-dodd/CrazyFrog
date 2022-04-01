using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;

    public float levelStartDelay = 2f;
    public int playerHealth = 100;

    private int level = 1;

    private GameObject levelImage;
    private Text levelText;

    public GameObject pauseMenu;

    private bool doingSetup;
    private bool paused;

    void Awake() {
        
        if (instance == null) {

            instance = this;

        } else if (instance != this) {

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();

    }

    void Start() {

        GameObject.FindGameObjectWithTag("Music").GetComponent<SoundManager>().PlayMusic();

    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (!paused) {

                PauseGame();

            } else {

                ResumeGame();

            }

        }

    }

    void OnLevelWasLoaded(int index) {

        level++;
        InitGame();

    }

    void InitGame() {

        doingSetup = true;
        paused = true;

        pauseMenu = GameObject.FindGameObjectWithTag("Pause");

        pauseMenu.SetActive(false);

        boardScript.SetupScene(level);

        levelImage = GameObject.Find("LevelCard");

        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + level;

        levelImage.SetActive(true);

        Invoke("HideLevelImage", levelStartDelay);

    }

    void HideLevelImage() {

        levelImage.SetActive(false);

        doingSetup = false;
        paused = false;
        
    }

    public void GameOver() {

        levelText.text = "You lost after " + level + " floors.";
        levelImage.SetActive(true);

        StartCoroutine(ReturnToMenu(5));

        enabled = false;

    }

    IEnumerator ReturnToMenu(float time) {

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("MainMenu");
 
    }

    public void PauseGame() {

        Time.timeScale = 0;
        paused = true;

        print("paused");
        pauseMenu.SetActive(true);

    }

    public void ResumeGame() {

        Time.timeScale = 1;
        paused = false;

        print("resumed");
        pauseMenu.SetActive(false);

    }
    
}
