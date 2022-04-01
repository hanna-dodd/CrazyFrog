using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int playerDamage;
    public int hp = 20;

    private Transform target;
    private bool skipMove;

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

}
