using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private BirdHandler birdHandler;
    private bool nextEvent = false;
    private void OnTriggerEnter(Collider other)
    {
       if(!nextEvent && other.tag == "Player")
        {
            print(other.tag);
            birdHandler.changeBird();
            nextEvent = true;
        }
    }

}
