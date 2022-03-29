using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattlePhase : MonoBehaviour
{
	public GameObject itemTemplate;

	public GameObject content;

	public GameObject viewList;

	public Player player;
	public Enemy opponent;
	//public spellData activeSpells;

	//Initialize necessary visual objects
	//I.E. public Text dialoguetext;

	public BattleState state;

	// Start is called before the first frame update
	public void Start()
	{
		state = BattleState.START;
	}

	public IEnumerator SetupBattle(Player activeplayer, Enemy currentEnemy)
	{
		//Initialize battle screen
		//Read in spellList and set it
		/* for (spell in spelllist) {
		 *      local copies of spell variables such as spellname, damage, accuracy, cost
		 *		var copy = Instantiate(itemTemplate);
		 *		copy.transform.SetParent(content.transform, false);
		 *		copy.GetComponentInChildren<Text>().text = spellname;
		 *		copy.GetComponent<Button>().onClick.AddListener(
		 *			() => 
		 *			{
		 *              StartCoroutine(PlayerSpellAttack(damage, cost, accuracy);
		 *			}
		 *		);
		 *		
		 * }
		   make the viewlist thing inactive	might need to happen before above code	*/
		player = activeplayer;
		opponent = currentEnemy;
		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator EnemyTurn()
	{
		yield return new WaitForSeconds(1f);

		player.LoseHP(opponent.playerDamage);

		bool isDead = player.CheckIfGameOver();

		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		}
		else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	IEnumerator EndBattle()
	{
		if (state == BattleState.WON)
		{
			//Display won text
			yield return new WaitForSeconds(2f);

		}
		else if (state == BattleState.LOST)
		{
			//Display lost text
			yield return new WaitForSeconds(2f);
			GameManager.instance.GameOver();
		}
	}

	IEnumerator PlayerTurn()
	{
		//Display text stating that battle has started
		yield return new WaitForSeconds(2f);
	}

	public void OnSlapButton()
    {
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerSlap());
	}

	public IEnumerator PlayerSlap()
    {
		bool isDead = opponent.DamageEnemy(player.physicalDamage);

		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnSpellButton()
	{
		if (state != BattleState.PLAYERTURN)
		{
			return;
		}
		;
		//set current dialogue/buttons to not active and call display spells
		//CHANGE TO DISPLAY SPELLS
	}

	/* public IEnumerator DisplaySpells()
	{
		//Set list of buttons to active
		//Buttons should activate next part of code if done right lets hope
	} */


		IEnumerator PlayerSpellAttack(int spelldamage, int spellcost, int successChance)
	{
		//This will need major editing due to no unique attack button/stat

		bool isDead = false;

		var accuracy = Random.Range(1, 100);

		if (successChance <= (accuracy/100))
        {
			isDead = opponent.DamageEnemy(spelldamage);

		}

		else
        {
			//notify that attack did not go through via dialogue
        }

		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

}
