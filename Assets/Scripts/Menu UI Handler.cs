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

    //private void Start()  //Sacar barras cuando funcione todo y guardar todo
    //{
      //  GameManager.Instance.SaveInfo();
    //}
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
