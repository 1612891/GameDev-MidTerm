using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    private int columnPoolSize = 0;
    public GameObject columnPrefab;
    public GameObject columnTreePrefab;

    public float spawnRate = 4f;
    private float comlumnMin = -2f;
    private float columnMax = 2f;

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-15f,-25f);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 10f;
    private int currentColumn = 0;

    // Start is called before the first frame update
    void Start()
    {
        columnPoolSize = GameControl.instance.scoreTheshold * 2;
        columns = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize / 2; i++)
        {
            columns[i] = (GameObject)Instantiate(columnTreePrefab, objectPoolPosition, Quaternion.identity);

        }
        for (int i = columnPoolSize / 2; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);

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
                columns[currentColumn].transform.position = new Vector2(spawnXPosition + 2.2f, spawnYPosition);
            else
                columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentColumn++;
            if (currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
    }
}
