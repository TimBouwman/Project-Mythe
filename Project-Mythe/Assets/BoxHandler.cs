using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    [SerializeField] private int amountToBeInBox;
    [SerializeField] private Door door;

    public void ItemInBox()
    {
        amountToBeInBox--;
        if(amountToBeInBox <= 0)
            door.CurrentInputAmount = 1;
    }
}