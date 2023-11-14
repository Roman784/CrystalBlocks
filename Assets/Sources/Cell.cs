using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Block OwnedBlock;
    public bool IsEmpty => OwnedBlock == null;
}
