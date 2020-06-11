using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBoxContainer : Container
{
    [Header("for cubebox")]
    [SerializeField] private GameObject correctObjectForContainer;
    [SerializeField] private BoxHandler boxHandler;
    private bool inbox = false;
    
    // Start is called before the first frame update


    // Update is called once per frame

    private void Update()
    {
        base.Update();
        if (transform.childCount >= 2 && !inbox)
        {
            if (transform.GetChild(1) != null && transform.GetChild(1).gameObject == correctObjectForContainer)
            {
                boxHandler.ItemInBox();
                inbox = true;
            }
        }
    }
}

