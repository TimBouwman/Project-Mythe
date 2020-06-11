using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayGame : Item
{
    [SerializeField] private GameObject BlackBox;
    public void Grab()
    {
        base.Grab();
        BlackBox.SetActive(true);
        Destroy(gameObject);
    }
}
