using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UB.Simple2dWeatherEffects.Standard;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public Text ScoreText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;
    public bool isFog = false;
    public int scoreTheshold = 2;
    private int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScore()
    {
        if (gameOver)
        {
            return;
        }
        score++;
        ScoreText.text = "Score: " + score.ToString();
        if (score % scoreTheshold == 0)
        {
            D2FogsPE d = Camera.main.GetComponent<D2FogsPE>();
            isFog = !isFog;
            if (isFog)
                d.Density = 1f;
            else
                d.Density = 0f;
            scrollSpeed *= 1.2f;
            GetComponent<ColumnPool>().spawnRate *= 0.83f;
        }
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;
    }
}
