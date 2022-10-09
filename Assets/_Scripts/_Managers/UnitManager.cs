using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    
    public Block blockHover;
    
    [SerializeField] private Tile slot;

    private List<ScriptableBlock> _blocks;
    private Queue<Block> _que;
    [SerializeField] private List<Block> _blocksOnTile;
    private List<Faction> _blockCombos;
    
    private int _count=1, _rock, _bishop, _dragon, _knight;
    
    private void Awake()
    {
        Instance = this;
        
        _que = new Queue<Block>();
        
        _blocks = Resources.LoadAll<ScriptableBlock>("Blocks").ToList();
    }

    private void Start()
    {
        _blocksOnTile = new List<Block>();
        _blockCombos = new List<Faction>();
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.PlayerTurn)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;

            var wordPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (!blockHover.IsUnityNull()) blockHover.transform.position = wordPos;
        }
        
    }

    public void InitSlot()
    {
        GenerateBlockSlot();
        GenerateBlockSlot();

        SetBlockHover();
    }

    public void UpdateSlot()
    {
        Destroy(_que.Peek().gameObject);
        _que.Dequeue();
        
        Destroy(blockHover.gameObject);
        
        GenerateBlockSlot();
        
        SetBlockHover();
    }

    private void SetBlockHover()
    {
        blockHover = Instantiate(_que.Peek());
        blockHover.name = "Block Hover";
        blockHover.GetComponent<SpriteRenderer>().sortingOrder = 2;
        _que.Peek().gameObject.SetActive(false);
    }

    private void GenerateBlockSlot()
    {
        var block = _blocks.OrderBy(o => Random.value).First().prefab;
        var spawnedBlock = Instantiate(block, slot.transform);

        spawnedBlock.name = $"BlockOnSlot";

        _que.Enqueue(spawnedBlock);
    }

    public bool GenerateBlockAtTile(Tile tile)
    {
        if (tile.occupiedBlock != null) return false;
        
        var spawnedBlock = Instantiate(_que.Peek());

        spawnedBlock.name = $"BlockOnTile {_count}";
        spawnedBlock.gameObject.SetActive(true);
        
        tile.SetBlock(spawnedBlock);
        
        _blocksOnTile.Add(spawnedBlock);
        _blockCombos.Add(spawnedBlock.faction);

        CheckCombo();
        CountEachBlock(spawnedBlock.faction);

        GameManager.Instance.timeRemaining = 10;
        _count++;
        
        return true;
    }

    private void CheckCombo()
    {
        if (_blocksOnTile.Count == 0) return;

        if (_blockCombos.Contains(Faction.Rock) && _blockCombos.Contains(Faction.Bishop))
        {
            ScoreManager.Instance.score += 2;
            _blockCombos.Remove(Faction.Rock);
            _blockCombos.Remove(Faction.Bishop);
        }
        
        if (_blockCombos.Contains(Faction.Knight) && _blockCombos.Contains(Faction.Dragon))
        {
            ScoreManager.Instance.score += 1;
            _blockCombos.Remove(Faction.Knight);
            _blockCombos.Remove(Faction.Dragon);
        }
    }

    private void CountEachBlock(Faction faction)
    {
        switch (faction)
        {
            case Faction.Bishop:
                _bishop++;
                if (_bishop == 3)
                {
                    _bishop = 0;
                    DeleteAllBlockByFaction(Faction.Bishop);
                }
                break;
            case Faction.Dragon:
                _dragon++;
                if (_dragon == 3)
                {
                    _dragon = 0;
                    DeleteAllBlockByFaction(Faction.Dragon);
                }
                break;
            case Faction.Knight:
                _knight++;
                if (_knight == 3)
                {
                    _knight = 0;
                    DeleteAllBlockByFaction(Faction.Knight);
                }
                break;
            case Faction.Rock:
                _rock++;
                if (_rock == 3)
                {
                    _rock = 0;
                    DeleteAllBlockByFaction(Faction.Rock);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(faction), faction, null);
        }
    }

    private void DeleteAllBlockByFaction(Faction faction)
    {
        var newBlocks = new List<Block>();
        foreach (var block in _blocksOnTile)
        {
            if(block.faction == faction)
            {
                Destroy(block.gameObject);
            }
            else
            {
                newBlocks.Add(block);
            }
        }

        _blocksOnTile = newBlocks;
    }
}
