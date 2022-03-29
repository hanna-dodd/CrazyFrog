using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SpellListMaker : MonoBehaviour
{
    public GameObject itemTemplate;

    public GameObject content;

    public void SpellButton_OnClick()
    {
        var copy = Instantiate(itemTemplate);
        copy.transform.SetParent(content.transform, false);

    }
}