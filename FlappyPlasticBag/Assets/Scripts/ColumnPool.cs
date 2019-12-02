using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize = 0;
    public GameObject columnPrefab;
    public GameObject columnTreePrefab;

    public float spawnRate = 4f;
    private float comlumnMin = -2f;
    private float columnMax = 2f;

    public GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-15f,-25f);
    private float timeSinceLastSpawned;
    private float spawnXPosition;
    private int currentColumn = 0;
    private float[] columnState;

    const float spawnX = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        columnPoolSize = GameControl.instance.scoreTheshold * 2;
        columns = new GameObject[columnPoolSize];
        columnState = new float[columnPoolSize];
        for (int i = 0; i < columnPoolSize / 2; i++)
        {
            columns[i] = (GameObject)Instantiate(columnTreePrefab, objectPoolPosition, Quaternion.identity);

        }
        for (int i = columnPoolSize / 2; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);

        }
        for (int i = 0; i < columnPoolSize; i++)
        {
            
            if (Random.Range(0, 10) > 5)
                columnState[i] = 1.0f;
            else
                columnState[i] = -1.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(comlumnMin, columnMax);
            if (currentColumn >= columnPoolSize / 2)
                spawnXPosition = spawnX + 2.2f;
            else
                spawnXPosition = spawnX;
            columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentColumn++;
            if (currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
        if (GameControl.instance.playMode == 1)
        {
            for (int i = 0; i < columnPoolSize; i++)
            {
                float y = columns[i].transform.position.y + columnState[i] * 0.025f;
                float x = columns[i].transform.position.x;
                columns[i].transform.position = new Vector2(x, y);
                if (y < comlumnMin || y > columnMax)
                    columnState[i] *= -1.0f;
            }
        }
    }
}
