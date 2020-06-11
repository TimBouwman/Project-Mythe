using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    [SerializeField] private int amountToBeInBox;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemInBox()
    {
        amountToBeInBox--;
        if(amountToBeInBox <= 0)
        {
            OpenDoor();
        }
    }
    private void OpenDoor()
    {
        print("hier komt straks de functie om deur te openen");
    }
}
