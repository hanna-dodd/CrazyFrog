using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MovingObject {

    public int enemyDamage = 1;
    public float restartLevelDelay = 1f;
    public TextMeshProUGUI healthText;
    public int physicalDamage = 1;

    private Animator animator;
    private int hp;

    protected override void Start() {

        animator = GetComponent<Animator>();
        hp = GameManager.instance.playerHP;

        healthText.text = "Health: " + hp;

        base.Start();
        
    }

    private void OnDisable() {

        GameManager.instance.playerHP = hp;

    }

    void Update() {

        if (!GameManager.instance.playersTurn) {

            return;

        }

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");

        if (horizontal != 0) {

            vertical = 0;

        }

        if (horizontal != 0 || vertical != 0) {

            AttemptMove<Enemy>(horizontal, vertical);

        }
        
    }

    protected override void AttemptMove<T>(int xDir, int yDir) {

        base.AttemptMove<T>(xDir, yDir);
        //RaycastHit2D hit;

        CheckIfGameOver();

        GameManager.instance.playersTurn = false;

    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Exit") {

            Invoke("Restart", restartLevelDelay);
            enabled = false;

        } else if (other.tag == "Scroll") {

            // add spell to player
            other.gameObject.SetActive(false);

        }

    }

    protected override void OnCantMove<T>(T component) {

        Enemy hitEnemy = component as Enemy;

        BattlePhase currentBattle = new BattlePhase();
        currentBattle.SetupBattle(this, hitEnemy);

    }

    private void Restart() {

        SceneManager.LoadScene(0);

    }

    public void LoseHP(int loss) {


        print("lost hp: " + loss);
        //set hit animation
        hp -= loss;
        healthText.text = "Health: " + hp;

    }

    public bool CheckIfGameOver(){

        if (hp <= 0) {

            return true;

        }
        else
        {
            return false;
        }
    }

}
