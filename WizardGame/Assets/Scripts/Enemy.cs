using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int playerDamage;
    public int hp = 200;

    private Transform target;
    private bool skipMove;

    public bool DamageEnemy(int loss) {

        hp -= loss;

        if (hp <= 0) {

            return true;

        }

        else
        {
            return false;
        }

    }

}
