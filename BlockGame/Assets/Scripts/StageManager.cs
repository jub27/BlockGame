using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public const int STAGE_WIDTH_BLOCK_COUNT = 12;
    public const int STAGE_HEIGHT_BLOCK_COUNT = 12;

    private ObjectPool<Block> blockObjectPool;

    [SerializeField]
    private Block blockPrefab;
    [SerializeField]
    private Transform stageTransform;

    private int[][] stageData;
    private int curStageBlockCount;

    public static StageManager instance = null; 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        blockObjectPool = new ObjectPool<Block>(blockPrefab, STAGE_WIDTH_BLOCK_COUNT * STAGE_HEIGHT_BLOCK_COUNT / 2);
        InitStageData();
    }

    private void InitStageData()
    {
        stageData = new int[STAGE_HEIGHT_BLOCK_COUNT][];
        for (int i = 0; i < stageData.Length; i++)
        {
            stageData[i] = new int[STAGE_WIDTH_BLOCK_COUNT];
        }
    }

    public void SetStage()
    {
        curStageBlockCount = 0;
        GetRandomStage();

        float blockWidth = Screen.width / stageData[0].Length;
        float blockHeight = Screen.height / stageData.Length;

        for (int y = 0; y < stageData.Length; y++)
        {
            for (int x = 0; x < stageData[y].Length; x++)
            {
                if(stageData[y][x] == 1)
                {
                    Block block = blockObjectPool.GetObject();
                    block.transform.SetParent(stageTransform);
                    block.SetHp(100);
                    block.SetSize(new Vector2(blockWidth, blockHeight));
                    block.SetPosition(new Vector2(blockWidth * x + blockWidth / 2.0f - blockWidth * STAGE_WIDTH_BLOCK_COUNT / 2.0f, -blockHeight * y - blockHeight / 2.0f + blockHeight * STAGE_HEIGHT_BLOCK_COUNT / 2.0f)); ;
                    curStageBlockCount++;
                }
            }
        }
    }

    public void ReturnObject(Block block)
    {
        blockObjectPool.ReturnObject(block);
        curStageBlockCount--;
    }

    private void GetRandomStage()
    {
        for (int y = 0; y < stageData.Length; y++)
        {
            for(int x = 0; x < stageData.Length; x++)
            {
                if (y >= STAGE_HEIGHT_BLOCK_COUNT / 3 && y < STAGE_HEIGHT_BLOCK_COUNT / 3 * 2
                    && x >= STAGE_WIDTH_BLOCK_COUNT / 3 && x < STAGE_WIDTH_BLOCK_COUNT / 3 * 2)
                    continue;
                stageData[y][x] = Random.Range(0, 2);
            }
        }
    }
}
