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

	//public GameObject viewList;

	public Text DialogueText;

	public PlayerController player;
	public Enemy opponent;

	public bool turnDone = false;


	public BattleState state;

	// Start is called before the first frame update
	public void Start()
	{
		//viewList.SetActive(false);

		state = BattleState.START;
	}

	public IEnumerator SetupBattle(PlayerController activeplayer, Enemy currentEnemy)
	{
		//Initialize battle screen
		/* for (int spellIndex = 0; spellIndex < activeplayer.learnedSpells.count; spellIndex++) {
		 *      curSpell = activeplayer.learnedSpells[spellIndex];
		 *		var copy = Instantiate(itemTemplate);
		 *		copy.transform.SetParent(content.transform, false);
		 *		copy.GetComponentInChildren<Text>().text = spellname;
		 *		copy.GetComponent<Button>().onClick.AddListener(
		 *			() => 
		 *			{
		 *				turnDone = true;
		 *              StartCoroutine(PlayerSpellAttack(curSpell.damage, curSpell.powerCost, curSpell.successChance, curSpell.spellname);
		 *			}
		 *		);
		 *		
		 * }
		   make the viewlist thing inactive	might need to happen before above code	*/
		player = activeplayer;
		opponent = currentEnemy;

		DialogueText.text = "An opponent approaches...";
		
		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator EnemyTurn()
	{
		DialogueText.text = "Your opponent prepares to strike";
		yield return new WaitForSeconds(1f);

		player.LoseHealth(opponent.playerDamage);

		bool isDead = player.CheckIfGameOver();

		DialogueText.text = "You were attacked for " + opponent.playerDamage.ToString() + " damage!";

		yield return new WaitForSeconds(2f);

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
			DialogueText.text = "You have won the battle!";
			yield return new WaitForSeconds(2f);
			gameObject.SetActive(false);

		}
		else if (state == BattleState.LOST)
		{
			DialogueText.text = "You have been defeated...";
			yield return new WaitForSeconds(2f);
			gameObject.SetActive(false);
			GameManager.instance.GameOver();
		}
	}

	IEnumerator PlayerTurn()
	{
		yield return new WaitForSeconds(2f);
		DialogueText.text = "";

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

		DialogueText.text = "You slapped your opponent for " + player.physicalDamage.ToString() + " damage!";

		yield return new WaitForSeconds(2f);

		player.AddHealth(5);

		DialogueText.text = "The slap restored " + player.physicalDamage.ToString() + " health!";

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

		//viewList.SetActive(true);

		print("You cast a spell");

		bool done = false;

		/* while (!turnDone)
		{
			if (turnDone)
            {
				break;
            }
		}
		*/
	}


	IEnumerator PlayerSpellAttack(int spelldamage, int spellcost, int successChance, string spellname)
	{

		bool isDead = false;

		var accuracy = Random.Range(1, 100);

		if (successChance <= (accuracy/100))
        {
			isDead = opponent.DamageEnemy(spelldamage);

			DialogueText.text = spellname + " hit for " + spelldamage.ToString() + " damage!";

			if (isDead)
			{
				state = BattleState.WON;
				EndBattle();
			}

		}

		else
        {
			DialogueText.text = spellname + " missed!";
        }

		yield return new WaitForSeconds(2f);

		if (spellcost > 0)
        {
			player.LoseHealth(spellcost);

			DialogueText.text = spellname + "'s cost drains " + spellcost.ToString() + " health!"; 

			if (player.CheckIfGameOver())
			{
				state = BattleState.LOST;
				EndBattle();
			}
		}

		yield return new WaitForSeconds(2f);
		
		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
		
	}

}
