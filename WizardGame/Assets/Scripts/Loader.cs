using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    void Awake() {

        if (GameManager.instance == null) {

            //Instantiate(gameManager);

            GameObject manager = Instantiate(gameManager);
            GameManager gm = manager.GetComponent<GameManager>();

        }
        
    }

}
