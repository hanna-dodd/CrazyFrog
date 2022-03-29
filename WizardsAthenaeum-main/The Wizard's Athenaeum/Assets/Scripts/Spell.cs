using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

    //public enum SpellType {damage, AOE, buff};
    public int damage;
    public int powerCost;
    
    public float successChance;

    public virtual void Cast() {

        //Unique to each spell

    }

}
