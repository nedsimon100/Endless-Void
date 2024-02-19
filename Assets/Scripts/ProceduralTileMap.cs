using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralTileMap : MonoBehaviour
{
    public Tilemap TM;
    public Tile Wall;

    public List<Vector3> enemySpawnPoints = new List<Vector3>();

    public int width = 200;
    public int height = 200;
    public int startDepth = -50;
    public float scale = 0.5f;

    public float WallNoise = 0.6f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private PlayerController Player;
    void Start()
    {
        offsetX = Random.Range(0f, 999f);
        offsetY = Random.Range(0f, 999f);
        Player = FindObjectOfType<PlayerController>();
        StartCoroutine(NewBuildDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NewBuildDelay()
    {
        while (true)
        {
            if (Player.transform.position.y < startDepth - (height/2))
            {
                this.transform.position = new Vector3Int(Mathf.FloorToInt(Player.transform.position.x - (width * 0.8659766f) / 2), Mathf.FloorToInt(Player.transform.position.y - height / 2), 0);
            }
            else
            {
                this.transform.position = new Vector3Int(Mathf.FloorToInt(Player.transform.position.x - (width * 0.8659766f) / 2), Mathf.FloorToInt(startDepth - (height)), 0);
            }
            
            BuildMap();
            yield return new WaitForSeconds(5f);
        }
    }

    public void BuildMap()
    {
        enemySpawnPoints.Clear();
        TM.size = new Vector3Int(width,height,0);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //float cellWorldX = (this.transform.position.x + x * 0.8659766f);
                //float cellWorldY = (this.transform.position.y + y);

                placeWall(x, y);
            }
        }
    }

    public void placeWall(int x,int y)
    {
        float xPos = (TM.CellToWorld(new Vector3Int(x, y, 0)).x) * scale + offsetX;
        float yPos = (TM.CellToWorld(new Vector3Int(x, y, 0)).y) * scale + offsetY;

        Vector3Int tilePosition = new Vector3Int(x, y, 0);
        if (Mathf.PerlinNoise(xPos, yPos) > WallNoise)
        {
            
            TM.SetTile(tilePosition, Wall);
        }
        else
        {
            
            TM.SetTile(tilePosition, null);
        }
        if(Mathf.PerlinNoise(xPos, yPos) < 0.1f&& (TM.CellToWorld(new Vector3Int(x, y, 0)) - Player.transform.position).magnitude>15)
        {
            enemySpawnPoints.Add(new Vector3((TM.CellToWorld(new Vector3Int(x, y, 0)).x), (TM.CellToWorld(new Vector3Int(x, y, 0)).y), 0));
        }
    }
    public int[] countPlayerScore(List<Vector3Int> placedTiles)
    {
        int[] playerCounts = new int[2];
        playerCounts[0] = 0;

        foreach(Vector3Int pt in placedTiles)
        {
            playerCounts[0]++;

            if(Mathf.PerlinNoise(pt.x*scale+offsetX, pt.y * scale + offsetY) > WallNoise)
            {
                playerCounts[1]++;
            }

        }
        return playerCounts;
    }
}
