using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner Instance;
    [SerializeField] private float delayBetweenBlocksSpawn=1f;
    [SerializeField] private Block[] blockPrefab;
    [SerializeField] private Transform spawnedBlocksPool;
    [SerializeField] private float blockLiveTime = 2f;
    [SerializeField]private List<Block> blocks = new List<Block>();
    private Vector3 blockScale;
    private bool isGameStarted;
    private bool isFirstBlockTimeSet;
    private Block firstBlock;
    private int countSpawnedBlocks;
    public int countBlockForLevel;
    private int score=1;
    private IDisposable spawner;
    public bool IsGameStarted
    {
        get { return isGameStarted; }
        set { isGameStarted = value; }
    }
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        PlayerController.onPlayerDeath += DisableSpawner;
        blockScale = blockPrefab[0].transform.localScale;
        if (isGameStarted)
        {
            spawner=Observable.Interval(System.TimeSpan.FromSeconds(delayBetweenBlocksSpawn)).TakeWhile(x => this.enabled).Subscribe(x => 
            { 
                CreateBlock();
                countSpawnedBlocks += 1;
                if (countSpawnedBlocks >= countBlockForLevel)
                {
                    DisableSpawner();
                }
            });
            if (!isFirstBlockTimeSet)
            {
                
                firstBlock.SetDeathTime(blockLiveTime);
                isFirstBlockTimeSet = true;
            }
        }
        else
        {
            CreateBlock(true);
            this.enabled = false;
        }

    }
    private void OnDisable()
    {
        PlayerController.onPlayerDeath -= DisableSpawner;
    }
    public void PlusSCore()
    {
        score += 1;
        if (score>=countBlockForLevel && countBlockForLevel>=5)
        {
            Debug.Log("WIN");
            UIController.instance.WinLevel();
        }
        
    }
    public void Restart()
    {
        spawner.Dispose();
        countSpawnedBlocks = 1;
        score = 1;
        isFirstBlockTimeSet= false;
        this.enabled = true;
    }
    public void SetBlockToSpawn(int blocks)
    {
        countBlockForLevel = blocks;
    }
    private void CreateBlock(bool isBlockFirst=false)
    {
        Block newBlock;
        if (isBlockFirst)
        {
            newBlock = Instantiate(blockPrefab[0], Vector3.zero, Quaternion.identity, spawnedBlocksPool);
            firstBlock = newBlock;
        }
        else
        {
            Vector3 spawnPosition = InstantiatePosition();
            newBlock = Instantiate(blockPrefab[UnityEngine.Random.Range(0,blockPrefab.Length)], spawnPosition, Quaternion.identity, spawnedBlocksPool);
            newBlock.GetComponent<ITemporary>().SetDeathTime(blockLiveTime);
        }
        blocks.Add(newBlock);
    }
    private Vector3 InstantiatePosition()
    {
        Vector3 previousBlock;
        if (blocks.Count > 0)
        {
            previousBlock = blocks[blocks.Count - 1].transform.position;
        }
        else
        {
            previousBlock = Vector3.zero;
        }
        Vector3 instPos = previousBlock;
        switch (ChooseRandomDirection())
        {

            case 1:
                instPos = new Vector3(instPos.x - blockScale.x, instPos.y, instPos.z);
                break;
            case 2:
                instPos = new Vector3(instPos.x, instPos.y, instPos.z + blockScale.z);
                break;
            case 3:
                instPos = new Vector3(instPos.x + blockScale.x, instPos.y, instPos.z);
                break;
            default:
                instPos = new Vector3(instPos.x - blockScale.x, instPos.y, instPos.z);
                break;
        }
        if (blocks.Count > 2)
        {
            if (instPos == blocks[blocks.Count - 2].transform.position)
            {
                instPos = new Vector3(previousBlock.x, previousBlock.y, previousBlock.z - blockScale.z);
            }
        }
            return instPos;
    }
    private int ChooseRandomDirection()
    {
        int randomNumber= UnityEngine.Random.Range(0, 4);
        return randomNumber;
    }
    private void DisableSpawner()
    {
        this.enabled=false;
        isGameStarted = false;
        blocks.Clear();
    }
}
