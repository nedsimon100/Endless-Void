using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private ProceduralTileMap TM;
    private List<Vector3> enemySpawnPoints = new List<Vector3>();
    private PlayerController player;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    void Start()
    {
        TM = FindObjectOfType<ProceduralTileMap>();
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(spawnTimer());
    }
    IEnumerator spawnTimer()
    {
        while (true)
        {
            if (player.transform.position.y < -60)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count - 1)], enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count - 1)], Quaternion.identity);
                yield return new WaitForSeconds(500 / (Mathf.Abs(player.transform.position.y)+20));
            }
            else
            {
                yield return null;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        enemySpawnPoints = TM.enemySpawnPoints;
    }
}
