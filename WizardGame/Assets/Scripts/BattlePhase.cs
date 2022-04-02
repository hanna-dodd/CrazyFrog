using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattlePhase : MonoBehaviour {

	public GameManager gameManager;

	public GameObject itemTemplate;

	public GameObject content;

	//public GameObject viewList;

	public Text dialogueText;
	public Text playerHealth;
	public Text enemyHealth;

	public GameObject spellButtons;

	public PlayerController player;
	public Enemy opponent;

	public bool turnDone = false;


	public BattleState state;

	void Start() {

		Time.timeScale = 1;

	}

	// Start is called before the first frame update
	public void SetupBattle() {

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

		dialogueText = GameObject.Find("Dialogue").GetComponent<Text>();
        dialogueText.text = "An opponent approaches...";

		playerHealth = GameObject.Find("PlayerHealth").GetComponent<Text>();
		enemyHealth = GameObject.Find("EnemyHealth").GetComponent<Text>();

		state = BattleState.PLAYERTURN;
		PlayerTurn();

	}

    public void PlayerTurn() {

        dialogueText.text = "Player turn!";

	}

    IEnumerator EnemyTurn() {

		dialogueText.text = "Your opponent prepares to strike";
		yield return new WaitForSeconds(1f);

		player.LoseHealth(10);
		playerHealth.text = "Health: " + player.health;

		bool isDead = player.CheckIfGameOver();

		dialogueText.text = "You were attacked for 10 damage";

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

    public void EndBattle() {

		if (state == BattleState.WON) {

			dialogueText.text = "You have won the battle!";
			gameManager.EndCombat();

		} else if (state == BattleState.LOST) {

			dialogueText.text = "You have been defeated...";
			gameManager.GameOver();

		}

	}

    public void OnSlapButton() {

		if (state != BattleState.PLAYERTURN)
			return;

        print("you slapped!");
		
		opponent.DamageEnemy(25);

		if (opponent.hp <= 0) {

			state = BattleState.WON;
			EndBattle();

		}
		
		enemyHealth.text = "Health: " + opponent.hp;

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

	}

    public void OnSpellButton() {

		if (state != BattleState.PLAYERTURN)
			return;

		print("Look at the spells");
		
		spellButtons.SetActive(true);

	}

	public void OnHealButton() {

		player.AddHealth(15);
		playerHealth.text = "Health: " + player.health;

		spellButtons.SetActive(false);

		state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

	}

	public void OnFireballButton() {

		opponent.DamageEnemy(100);
		enemyHealth.text = "Health: " + opponent.hp;

		if (opponent.hp <= 0) {

			state = BattleState.WON;
			EndBattle();

		}

		spellButtons.SetActive(false);

		state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

	}

}
