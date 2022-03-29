using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {

    public GameObject startText;
    public GameObject secondText;
    public GameObject lastText;

    public GameObject frog;
    public GameObject player;
    public GameObject wizard;

    private int scene = 0;
    private int speed = 5;

    private RectTransform playerTransform;

    private Vector3 mid = new Vector3(1000, 0, 0);

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.5f;

    void Start() {

        playerTransform = player.GetComponent<RectTransform>();

        StartCoroutine(FirstScene(2));

        StartCoroutine(SecondScene(10));

        StartCoroutine(ThirdScene(20));

        StartCoroutine(EndScene(30));
        
    }

    void Update() {

        if (scene == 1) {

            playerTransform.localPosition = Vector3.SmoothDamp(playerTransform.localPosition, mid, ref velocity, smoothTime);

        } else if (scene == 2) {



        } else if (scene == 3) {



        }

    }

    IEnumerator FirstScene(float time) {

        yield return new WaitForSeconds(time);

        startText.SetActive(true);
        player.SetActive(true);

        // move player object across screen from left to right
        scene = 1;

    }

    IEnumerator SecondScene(float time) {

        yield return new WaitForSeconds(time);

        startText.SetActive(false);
        secondText.SetActive(true);

        // have wizard sprite w. enemies follow player sprite across screen right to left
        // wizard moves halfway then moves back right
        scene = 2;

    }

    IEnumerator ThirdScene(float time) {

        yield return new WaitForSeconds(time);

        secondText.SetActive(false);
        lastText.SetActive(true);

        // have frog sprite move across screen left to right and stand in the middle
        scene = 3;

    }

    IEnumerator EndScene(float time) {

        yield return new WaitForSeconds(time);

        lastText.SetActive(false);

        SceneManager.LoadScene("LevelGeneration");

    }

}
