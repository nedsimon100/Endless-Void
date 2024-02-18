using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawMap : MonoBehaviour
{
    List<Vector3Int> drawnCells = new List<Vector3Int>();
    List<Vector3Int> workingMap = new List<Vector3Int>();
    public Tilemap PTM;
    public Tile Wall;
    public bool build = true;
    private PlayerController Player;
    public int width = 100;
    public int height = 100;
    public bool delete = false;
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        StartCoroutine(NewBuildDelay());
    }
    IEnumerator NewBuildDelay()
    {
        while (true)
        {
          
            this.transform.position = new Vector3Int(Mathf.FloorToInt(Player.transform.position.x - (width * 0.8659766f) / 2), Mathf.FloorToInt(Player.transform.position.y - height / 2), 0);

            BuildMap();

            yield return new WaitForSeconds(5f);
        }
    }

    public void BuildMap()
    {
        PTM.size = new Vector3Int(width, height, 0);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int cellWorldX = Mathf.FloorToInt(this.transform.position.x + x * 0.8659766f);
                int cellWorldY = Mathf.FloorToInt(this.transform.position.y + y);
                Vector3Int tilePosition = PTM.WorldToCell(new Vector3(cellWorldX, cellWorldY, 0));
                if (drawnCells.Contains(new Vector3Int(cellWorldX, cellWorldY, 0))||(workingMap.Contains(new Vector3Int(cellWorldX, cellWorldY, 0)) && !delete)) 
                { 
                    PTM.SetTile(tilePosition, Wall);
                }
                else
                {
                    PTM.SetTile(tilePosition, null);
                }
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        playerDraw();
        
    }
    public void playerDraw()
    {
        Vector3 MP = Camera.main.ScreenToWorldPoint(new Vector3((Input.mousePosition.x), (Input.mousePosition.y), 0));
        Vector3Int cell = new Vector3Int(Mathf.FloorToInt(MP.x), Mathf.FloorToInt(MP.y), 0);
        if (Input.GetMouseButtonDown(0) && build)
        { 
            if (!drawnCells.Contains(cell))
            {
                delete = false;
            }
            else
            {
                delete = true;
            }
            workingMap.Add(cell);
        }
        if (Input.GetMouseButton(0) && build)
        {
            if (!drawnCells.Contains(cell) && !delete)
            {
                workingMap.Add(cell);
            }
            else if (delete && drawnCells.Contains(cell))
            {
                workingMap.Add(cell);
            }
        }
        if (Input.GetMouseButtonUp(0) && build)
        {
            if (!delete)
            {
                foreach (Vector3Int wmap in workingMap)
                {
                    drawnCells.Add(wmap);
                }
            }
            if (delete)
            {
                foreach (Vector3Int wmap in workingMap)
                {
                    drawnCells.Remove(wmap);
                }
            }
            workingMap.Clear();
        }
        BuildMap();
    }
    
}
