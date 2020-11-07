using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BlockSpawner : MonoBehaviour
{
    public Block blockPrefab;
    public Transform maxBlockPos;
    public Transform minBlockPos;
    public Transform returnPos;
    public int maxBlockInPool = 5;
    public float blockMoveSpeed = 1;
    public float intervalTime = 1;
    
    [SerializeField] private bool ready = false;
    
    private List<Block> blocks = new List<Block>();
    private int currentBlockIndex = 0;
    private float spawnTimeRemaining = 0;

    public void Enable(bool value)
    {
        ready = value;

        if (!value)
        {
            blocks.ForEach(x => x.Stop());
        }
    }
    
    private void Start()
    {
        blocks = new List<Block>();
        for (int i = 0; i < maxBlockInPool; i++)
        {
            var b = Instantiate(blockPrefab, transform);
            b.Setup(transform.position, blockMoveSpeed, returnPos);
            b.gameObject.SetActive(false);
            blocks.Add(b);
        }
    }

    private void Update()
    {
        if (ready && blocks.Count > 0)
        {
            spawnTimeRemaining -= Time.deltaTime;
            if (spawnTimeRemaining <= 0)
            {
                spawnTimeRemaining = intervalTime;
                Vector2 pos = new Vector2(transform.position.x, UnityEngine.Random.Range(minBlockPos.position.y, maxBlockPos.position.y));
                Block b = blocks[currentBlockIndex];
                b.Setup(pos, blockMoveSpeed);
                b.gameObject.SetActive(true);
                currentBlockIndex = (currentBlockIndex + 1) % blocks.Count;
            }
        }
    }
}
