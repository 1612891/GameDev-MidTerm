using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UB.Simple2dWeatherEffects.Standard;
using System.IO;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public GameObject gameHighScoreText;
    public Text highScoreText;
    public Text ScoreText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;
    public bool isFog = false;
    public int scoreTheshold = 2;

    public AudioSource flapSound;
    public AudioSource hitSound;
    public AudioSource dieSound;
    public AudioSource pointSound;
    public AudioSource changeSound;
    private int score = 0;

    public GameObject polluter;
    public GameObject seed;
    public GameObject activeObject;

    public bool isSwitch = false;
    public bool isExit = false;

    void GoToHell(GameObject go)
    {
        go.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        go.GetComponent<PolygonCollider2D>().enabled = false;
    }

    void BringToLife(GameObject go)
    {
        go.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        go.GetComponent<PolygonCollider2D>().enabled = true;
        activeObject = go;
    }
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
        BringToLife(polluter);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else
        {
            if (isExit && isSwitch)
            {
                changeSound.Play();
                Debug.Log("Communism iz shit " + isExit.ToString() + isSwitch.ToString());
                if (activeObject == polluter)
                {
                    GoToHell(polluter);
                    BringToLife(seed);
                }
                else
                {
                    GoToHell(seed);
                    BringToLife(polluter);
                }
                isSwitch = false;
                //isExit = false;
            }
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
        pointSound.Play();
        if (score % scoreTheshold == 0)
        {
            isSwitch = true;
            D2FogsPE d = Camera.main.GetComponent<D2FogsPE>();
            isFog = !isFog;
            if (isFog)
            {
                d.Density = 1f;
            }
            else
            {
                d.Density = 0f;
            }
            scrollSpeed *= 1.2f;
            GetComponent<ColumnPool>().spawnRate *= 0.83f;
            
        }
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        gameHighScoreText.SetActive(true);
        //string path = "Assets/Resources/high_score.txt";
        //StreamReader reader = new StreamReader(path);
        //string content = reader.ReadToEnd();
        //reader.Close();
        //int highScore = int.Parse(content);
        //if (highScore <= score)
        //{
        //    highScore = score;
        //}
        //highScoreText.text = highScore.ToString();
        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine(highScore.ToString());
        //writer.Close();
        int highScore = PlayerPrefs.GetInt("high_score", 0);
        if (highScore <= score)
        {
            highScore = score;
        }
        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt("high_score", highScore);
        gameOver = true;
    }
}
