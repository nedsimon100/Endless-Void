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
        TM.size = new Vector3Int(width,height,0);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (placeWall(x, y))
                {
                    float cellWorldX = (this.transform.position.x + x);
                    float cellWorldY = (this.transform.position.y + y);

                    // Set tile using world position of the cell
                    Vector3Int tilePosition = TM.WorldToCell(new Vector3(cellWorldX, cellWorldY, 0));
                    TM.SetTile(tilePosition, Wall);
                }
                else
                {
                    float cellWorldX = (this.transform.position.x + x);
                    float cellWorldY = (this.transform.position.y + y);

                    // Set tile using world position of the cell
                    Vector3Int tilePosition = TM.WorldToCell(new Vector3(cellWorldX, cellWorldY, 0));
                    TM.SetTile(tilePosition, null);
                }
            }
        }
    }

    public bool placeWall(int x,int y)
    {
        float xPos = Mathf.Floor(this.transform.position.x + (x* 0.8659766f)) * scale + offsetX;
        float yPos = Mathf.Floor(this.transform.position.y + y) * scale + offsetY;

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
