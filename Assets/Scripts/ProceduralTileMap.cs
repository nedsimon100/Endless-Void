using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralTileMap : MonoBehaviour
{
    public Tilemap TM;
    public Tile Wall;

    public int width = 200;
    public int height = 200;

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
            this.transform.position = new Vector3Int(Mathf.FloorToInt(Player.transform.position.x - (width / 4)), Mathf.FloorToInt(Player.transform.position.y - (height / 5)), 0);
            BuildMap();
            yield return new WaitForSeconds(5f);
        }
    }

    public void BuildMap()
    {
        TM.size = new Vector3Int(width,height,0);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (placeWall(x, y))
                {
                    TM.SetTile(new Vector3Int(Mathf.FloorToInt(this.transform.position.x+x),Mathf.FloorToInt(this.transform.position.y+y),0), Wall);
                }
                else
                {
                    TM.SetTile(new Vector3Int(Mathf.FloorToInt(this.transform.position.x + x), Mathf.FloorToInt(this.transform.position.y + y), 0), null);
                }
            }
        }
    }

    public bool placeWall(int x,int y)
    {
       float  xPos = x*scale+ Mathf.FloorToInt( this.transform.position.x)+offsetX;
       float yPos = y*scale+Mathf.FloorToInt(this.transform.position.y)+offsetY;

        if (Mathf.PerlinNoise(xPos, yPos) > WallNoise)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
