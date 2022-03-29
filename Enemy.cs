using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {

    public int playerDamage;
    public int hp = 3;

    private Transform target;
    private bool skipMove;

    protected override void Start() {

        GameManager.instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
        
    }

    protected override void AttemptMove<T>(int xDir, int yDir) {

        if (skipMove) {

            skipMove = false;
            return;

        }

        base.AttemptMove<T>(xDir, yDir);

        skipMove = true;

    }

    public void MoveEnemy() {

        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon) {

            yDir = target.position.y > transform.position.y ? 1 : -1;

        } else {

            xDir = target.position.x > transform.position.x ? 1 : -1;

        }
        //Here is the attempt to move that is called from main script, may need to return value here or integrate combat phase
        AttemptMove<Player>(xDir, yDir);

    }

    public bool DamageEnemy(int loss) {

        hp -= loss;

        if (hp <= 0) {

            gameObject.SetActive(false);
            return true;

        }

        else
        {
            return false;
        }

    }

    protected override void OnCantMove<T>(T component) {

        Player hitPlayer = component as Player;
        BattlePhase currentBattle = new BattlePhase();
        currentBattle.SetupBattle(hitPlayer, this);
        //Here is where contact with player has a reaction
        //hitPlayer.LoseHP(playerDamage);

    }

}
