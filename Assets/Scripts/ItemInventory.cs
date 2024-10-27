using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public int Id;
    public string Type;
    public string Description;
    public Sprite Icon;
    [HideInInspector]
    public bool PickedUp;
   
}
