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
    public Text superText;
    public Text durationText;
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
    private int super = 0;

    public GameObject polluter;
    public GameObject seed;
    public GameObject activeObject;

    public bool isSuper;
    public float superDuration;

    public bool isSwitch = false;
    public bool isExit = false;
    private float spawnRate = 4f;
    private float alphaSpawn = 0.83f;


    public int playMode;
    float alpha = 1.2f;

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
        playMode = PlayerPrefs.GetInt("mode", 0);
        switch (playMode)
        {
            case 0:
                scrollSpeed = -1.5f;
                alpha = 1.2f;
                spawnRate = 4f;
                alphaSpawn = 0.83f;
                break;
            case 1:
                scrollSpeed = -2.592f;
                alpha = 1.0f;
                spawnRate = 2.3f;
                alphaSpawn = 1.0f;
                break;

        }
        GoToHell(seed);
        BringToLife(polluter);
    }

    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else
        {
            superText.text = "Super x" + super.ToString();
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
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ActiveSuperHero();
            }
            if (isSuper)
            {
                superDuration -= Time.deltaTime;
                Debug.Log(superDuration);
                if (superDuration <= 0)
                {
                    isSuper = false;
                    superDuration = 0f;
                }
                for (int i = 0; i < GetComponent<ColumnPool>().columnPoolSize; i++)
                {
                    Physics2D.IgnoreCollision(polluter.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[0], true);
                    Physics2D.IgnoreCollision(polluter.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[1], true);
                    Physics2D.IgnoreCollision(seed.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[0], true);
                    Physics2D.IgnoreCollision(seed.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[1], true);
                }
                durationText.text = superDuration.ToString() + "s";
            } else
            {
                for (int i = 0; i < GetComponent<ColumnPool>().columnPoolSize; i++)
                {
                    Physics2D.IgnoreCollision(polluter.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[0], false);
                    Physics2D.IgnoreCollision(polluter.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[1], false);
                    Physics2D.IgnoreCollision(seed.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[0], false);
                    Physics2D.IgnoreCollision(seed.GetComponent<PolygonCollider2D>(), GetComponent<ColumnPool>().columns[i].GetComponentsInChildren<PolygonCollider2D>()[1], false);
                }
            }
        }
    }

    public void ActiveSuperHero()
    {
        if (super > 0)
        {
            super -= 1;
            isSuper = true;
            if (playMode == 1)
                superDuration += 5f;
            else
                superDuration += 3f;
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
            super += 1;
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

            scrollSpeed *= alpha;
            spawnRate *= alphaSpawn;
            GetComponent<ColumnPool>().spawnRate = spawnRate;
            
        }
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        gameHighScoreText.SetActive(true);
        int highScore = PlayerPrefs.GetInt("high_score_" + playMode.ToString(), 0);
        if (highScore <= score)
        {
            highScore = score;
        }
        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt("high_score_" + playMode.ToString(), highScore);
        gameOver = true;
    }
}
