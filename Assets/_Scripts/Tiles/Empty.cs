using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Empty : Tile
{
    
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject attackZone;

    private Block _blockZone;
    
    private void OnMouseEnter()
    {
        if (GameManager.Instance.gameState != GameState.PlayerTurn) return;
        
        SetHighlight(true);
        GridManager.Instance.tileSelected = this;
        
        GenerateAttackZone();
    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.gameState != GameState.PlayerTurn) return;
        
        SetHighlight(false);
        Destroy(_blockZone.gameObject);
        _blockZone.ClearAttackZone();
    }
    
    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.PlayerTurn) return;
        
        if(!UnitManager.Instance.GenerateBlockAtTile(this)) return;
        
        if (_blockZone.CheckOccupied())
        {
            Destroy(UnitManager.Instance.blockHover.gameObject);
            return;
        }
        
        UnitManager.Instance.UpdateSlot();
        
        // Black zone
        Destroy(_blockZone.gameObject);
        _blockZone.ClearAttackZone();
        GenerateAttackZone();
        
    }

    public void SetHighlight(bool option)
    {
        highlight.SetActive(option);
    }

    override public void SetAttackZone(bool option)
    {
        attackZone.SetActive(option);
    }

    private void GenerateAttackZone()
    {
        _blockZone = Instantiate(UnitManager.Instance.blockHover, transform.position, quaternion.identity);
        // Destroy(_blockZone.GetComponent<SpriteRenderer>());
        _blockZone.gameObject.SetActive(false);
        _blockZone.occupiedTile = this;
        _blockZone.ShowAttackZone();
    }
    
}
