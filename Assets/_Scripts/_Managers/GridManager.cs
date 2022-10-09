using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public Tile tileSelected;
    
    [SerializeField] private int width, height;

    [SerializeField] private Tile tile;

    private Dictionary<Vector2, Tile> _tiles;
    
    
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _tiles = new Dictionary<Vector2, Tile>();
    }

    private void Update()
    {
        if (!tileSelected.IsUnityNull())
        {
            
        }
        
    }

    public void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tile, transform);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.transform.position += new Vector3(x, -y, 0);
                
                // save x and y
                spawnedTile.position = new Vector2(x, y);

                _tiles.Add(new Vector2(x, y), spawnedTile);
            }
        }
        
        GameManager.Instance.ChangeGameState(GameState.PlayerTurn);
    }

    public Tile GetTile(float x, float y)
    {
        return _tiles.TryGetValue(new Vector2(x,y), out var newTile) ? newTile : null;
    }
}
