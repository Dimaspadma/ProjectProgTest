using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Tile occupiedTile;
    public Faction faction;

    protected List<Tile> listTile;

    private void Awake()
    {
        listTile = new List<Tile>();
    }

    public virtual void ShowAttackZone()
    {
        
    }

    public void ClearAttackZone()
    {
        foreach (var tile in listTile)
        {
            if(tile != null) tile.SetAttackZone(false);
        }
        listTile.Clear();
    }

    public bool CheckOccupied()
    {
        foreach (var tile in listTile)
        {
            if (tile.IsUnityNull()) continue;
            
            if(tile.occupiedBlock != null)
            {
                GameManager.Instance.ChangeGameState(GameState.GameOver);
                return true;
            }
        }

        return false;
    }
}
