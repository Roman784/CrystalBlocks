using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDestroyer : MonoBehaviour
{
    public void Destroy(HashSet<Block> blocks)
    {
        foreach (Block block in blocks)
        {
            block.Destroy();
        }
    }
}
