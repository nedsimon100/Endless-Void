using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawMap : MonoBehaviour
{
    List<Vector3Int> drawnCells = new List<Vector3Int>();
    public Tilemap PTM;
    public Tile Wall;
    public bool build = true;
    private PlayerController Player;
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
        TM.size = new Vector3Int(width, height, 0);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 MP = Camera.main.ScreenToWorldPoint(new Vector3((Input.mousePosition.x), (Input.mousePosition.y), 0));
        Vector3Int cell = new Vector3Int(Mathf.FloorToInt(MP.x), Mathf.FloorToInt(MP.y), 0);
        if (Input.GetMouseButton(0)&&build)
        {
            if (!drawnCells.Contains(cell))
            {
                drawnCells.Add(cell);
            }
            else
            {
                drawnCells.Remove(cell);
            }
        }
    }
    
}
