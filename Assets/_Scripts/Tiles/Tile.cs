using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public Vector2 position;
    
    public Block occupiedBlock;
    public void SetBlock(Block block)
    {
        occupiedBlock = block;

        block.occupiedTile = this;
        block.transform.position = transform.position;
    }
    
    public virtual void SetAttackZone(bool option)
    {
    }
}
