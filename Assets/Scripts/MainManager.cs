using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    [SerializeField]private int m_Points;
    [SerializeField]private int m_MaxPoints;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(bestScoreText.text);

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        if (GameManager.Instance != null)
        {
           
            bestScoreText.text = $"Score : {GameManager.Instance.playerNameText : GameManager.Instance.playerScore}";
            Debug.Log("Este es el nombre" + bestScoreText.text);
        }
        else
        {
            Debug.Log("No hay GameManager");
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);

                if (m_MaxPoints > 0)
                {
                    bestScoreText.text = $"Best Score : {GameManager.Instance.playerName} : {m_MaxPoints}";
                }
            }
        }
        else if (m_GameOver)
        {



            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }


    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        
        if (m_Points > GameManager.Instance.bestScore)
            {
            GameManager.Instance.bestScore = m_Points;
            GameManager.Instance.playerScore = m_Points;
            GameManager.Instance.playerNameText = GameManager.Instance.playerNameText;
            
            bestScoreText.text = $"Best Score : {GameManager.Instance.playerName} : {GameManager.Instance.bestScore}";

            GameManager.Instance.SaveInfo();
            }
            // SceneManager.LoadScene(0);
    }
}
