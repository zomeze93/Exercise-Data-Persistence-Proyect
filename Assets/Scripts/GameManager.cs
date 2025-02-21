using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField playerName;
    public int playerScore;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("Grabado nombre: " + playerName.text);
    //     }
    // }


    [System.Serializable]
class SaveData
{
    public string playerName;
    public int playerScore;
}
    public void SaveInfo()
    {
        if (playerName == null)
        {


        SaveData data = new SaveData();
        data.playerName = playerName.text;
        data.playerScore = playerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        else
        {
            Debug.Log("playerName es null en SaveInfo(). No se guardará el nombre.");
        }
    }


    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (playerName != null) playerName.text = data.playerName;
            playerScore = data.playerScore;
        }
    }


}
