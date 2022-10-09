using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Block
{
    public override void ShowAttackZone()
    {
        var pos = occupiedTile.position;
        listTile.Add(GridManager.Instance.GetTile(pos.x+1, pos.y));
        listTile.Add(GridManager.Instance.GetTile(pos.x-1, pos.y));
        listTile.Add(GridManager.Instance.GetTile(pos.x, pos.y+1));
        listTile.Add(GridManager.Instance.GetTile(pos.x, pos.y-1));
        listTile.Add(GridManager.Instance.GetTile(pos.x-1, pos.y-1));
        listTile.Add(GridManager.Instance.GetTile(pos.x+1, pos.y+1));
        listTile.Add(GridManager.Instance.GetTile(pos.x+1, pos.y-1));
        listTile.Add(GridManager.Instance.GetTile(pos.x-1, pos.y+1));

        foreach (var tile in listTile)
        {
            if(tile != null) tile.SetAttackZone(true);
        }
    }

}
