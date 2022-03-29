using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float restartLevelDelay = 1f;

    private int health;
    private int maxHealth = 100;

    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    public Animator anim;

    void Start() {

        movePoint.parent = null;

        health = GameManager.instance.playerHealth;
        
    }

    private void OnDisable() {

        GameManager.instance.playerHealth = health;

    }

    void Update() {

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f) {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {

                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, whatStopsMovement)) {

                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                }

            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {

                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, whatStopsMovement)) {

                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                }

            }

            anim.SetBool("moving", false);

        } else {

            anim.SetBool("moving", true);

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Exit") {

            Invoke("Restart", restartLevelDelay);

            enabled = false;

        } else if (other.tag == "Scroll") {

            // add spell to spell list
            Destroy(other.gameObject);

        } else if (other.tag == "Food") {

            Destroy(other.gameObject);
            AddHealth(10);
            print(health);
        
        } else if (other.tag == "Enemy") {

            //start combat
            LoseHealth(20);
            print(health);

        }

    }

    private void Restart() {

        SceneManager.LoadScene(0);

    }

    public void LoseHealth(int loss) {

        health -= loss;
        CheckIfGameOver();

    }

    public void AddHealth(int add) {

        health += add;

        if (health > maxHealth) {

            health = maxHealth;

        }

    }

    private void CheckIfGameOver() {

        if (health <= 0) {

            GameManager.instance.GameOver();

        }

    }

}
