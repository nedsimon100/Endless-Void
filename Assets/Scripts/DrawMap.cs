using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawMap : MonoBehaviour
{
    List<Vector3> drawnCells = new List<Vector3>();
    List<Vector3> workingMap = new List<Vector3>();
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
          
            this.transform.position = new Vector3Int(Mathf.FloorToInt(Player.transform.position.x - ((width * 0.8659766f) / 2)), Mathf.FloorToInt(Player.transform.position.y - (height / 2)), 0);
            BuildMap();



            yield return new WaitForSeconds(5f);
        }
    }

    public void BuildMap()
    {
        PTM.size = new Vector3Int(width, height, 0);
        PTM.ClearAllTiles();

        
        if (!delete)
        {
            foreach (Vector3 DC in drawnCells)
            {
                if (PTM.WorldToCell(DC).x < width && PTM.WorldToCell(DC).x > 0 && PTM.WorldToCell(DC).y < height && PTM.WorldToCell(DC).y > 0)
                {
                    PTM.SetTile(PTM.WorldToCell(DC), Wall);
                }

            }
            foreach (Vector3 WM in workingMap)
            {
                if (PTM.WorldToCell(WM).x < width && PTM.WorldToCell(WM).x > 0 && PTM.WorldToCell(WM).y < height && PTM.WorldToCell(WM).y > 0)
                {
                    PTM.SetTile(PTM.WorldToCell(WM), Wall);
                }

            }
        }
        else
        {
            foreach (Vector3 DC in drawnCells)
            {
                if (PTM.WorldToCell(DC).x < width && PTM.WorldToCell(DC).x > 0 && PTM.WorldToCell(DC).y < height && PTM.WorldToCell(DC).y > 0 && !workingMap.Contains(DC))
                {
                    PTM.SetTile(PTM.WorldToCell(DC), Wall);
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
        Vector3 cell = PTM.CellToWorld(PTM.WorldToCell(MP));
        
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
            if ((!drawnCells.Contains(cell) && !delete)||(delete && drawnCells.Contains(cell)))
            {
                workingMap.Add(cell);
            }
            BuildMap();
        }
        if (Input.GetMouseButtonUp(0) && build)
        {
            if (!delete)
            {
                foreach (Vector3 wmap in workingMap)
                {
                    drawnCells.Add(wmap);
                }
            }
            if (delete)
            {
                foreach (Vector3 wmap in workingMap)
                {
                    drawnCells.Remove(wmap);
                }
            }
            workingMap.Clear();
        }
        
    }
    
}
