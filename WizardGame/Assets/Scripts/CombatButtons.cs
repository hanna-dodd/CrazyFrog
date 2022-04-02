using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatButtons : MonoBehaviour {

    public BattlePhase battlePhase;

    public void Attack() {

        battlePhase.OnSlapButton();

    }

    public void CastSpell() {

        battlePhase.OnSpellButton();

    }

    public void Heal() {

        battlePhase.OnHealButton();

    }

    public void Fireball() {

        battlePhase.OnFireballButton();

    }

}
