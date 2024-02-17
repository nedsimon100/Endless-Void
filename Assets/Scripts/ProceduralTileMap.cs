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
            this.transform.position = new Vector3Int(Mathf.FloorToInt(Player.transform.position.x - 50f), Mathf.FloorToInt(Player.transform.position.y - 50f), 0);
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
        float xPos = (this.transform.position.x + x) * scale + offsetX;
        float yPos = (this.transform.position.y + y) * scale + offsetY;

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
