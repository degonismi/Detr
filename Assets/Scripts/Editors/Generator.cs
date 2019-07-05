using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
[ExecuteInEditMode]
public class Generator : MonoBehaviour
{

    public Vector3 BlockPos;
    private Vector3 MapPos;
    private int NextBlock;
    public GameObject Block;
    public int BlockCount;
    public int Num;
    private int _temp;

    public int LineNumber;
    
    [ContextMenu("Add Block")]
    public void AddBlock()
    {
        MapPos = BlockPos;
        _temp = BlockCount;

        while (LineNumber>0)
        {
            while (BlockCount>0)
            {
                NextBlock = Random.Range(0, 2);
                if (NextBlock == 0)
                {
                    BlockPos = new Vector3(BlockPos.x-1,BlockPos.y-1, BlockPos.z);
                }
                else
                {
                    BlockPos = new Vector3(BlockPos.x,BlockPos.y-1, BlockPos.z-1);
                }

                GameObject newBlock = Instantiate(Block, BlockPos, Quaternion.identity);
                newBlock.name = "Block_"+ Num;
                newBlock.transform.SetParent(transform, true);
                BlockCount--;
            }
            BlockPos = MapPos;
            Num++;
            LineNumber--;
            BlockCount = _temp;
        }
        
        

    } 
    
}
