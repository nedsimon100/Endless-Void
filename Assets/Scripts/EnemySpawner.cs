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
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)], Quaternion.identity);
            yield return new WaitForSeconds(3000/Mathf.Abs(player.transform.position.y));
        }
    }
    // Update is called once per frame
    void Update()
    {
        enemySpawnPoints = TM.enemySpawnPoints;
    }
}
