using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // private void Start()
    // {
    //     scoreText.text = "Best Score: " + GameManager.Instance.playerName + ":" + GameManager.Instance.playerScore;
    // }
    public void StarNew()
    {
        SceneManager.LoadScene(1);
    }

    public void newNameSelected(string name)
    {
        GameManager.Instance.playerName.text = name;
    }

    public void SaveNamePlay()
    {
        GameManager.Instance.SaveInfo();
    }
}
