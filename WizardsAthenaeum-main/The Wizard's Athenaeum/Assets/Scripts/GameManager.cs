using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public float turnDelay = 0.1f;
    public static GameManager instance = null;
    public int playerHP = 100;
    [HideInInspector] public bool playersTurn = true;

    private BoardManager boardScript;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private TextMeshProUGUI levelText;
    private GameObject levelCard;
    private bool setup;

    void Awake() {

        if (instance == null) {

            instance = this;

        } else if (instance != this) {

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        
        InitGame();
        
    }

    void OnLevelWasLoaded(int index) {

        level++;
        InitGame();

    }

    void InitGame() {

        setup = true;

        levelCard = GameObject.Find("LevelCard");
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();

        levelText.text = "Level " + level;
        levelCard.SetActive(true);
        Invoke("HideLevelCard", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(level);

    }

    private void HideLevelCard() {

        levelCard.SetActive(false);
        setup = false;

    }

    public void GameOver() {

        levelText.text = "You have failed";
        levelCard.SetActive(true);

        enabled = false;

    }

    void Update() {

        if (playersTurn || enemiesMoving || setup) {

            return;

        }

        StartCoroutine(MoveEnemies());

    }

    public void AddEnemyToList(Enemy script) {

        enemies.Add(script);

    }

    IEnumerator MoveEnemies() {

        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0) {

            yield return new WaitForSeconds(turnDelay);

        }

        for (int i = 0; i < enemies.Count; i++) {

            //Here is where potential contact can happen
            //Making another class may be better tbh
            //if (enemies[i].MoveEnemy() returns true)
            //start battle phase and halt current processes, must include remaining code left to do in battle phase
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);

        }

        playersTurn = true;
        enemiesMoving = false;

    }
    
}
