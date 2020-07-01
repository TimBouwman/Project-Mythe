using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameTrigger : MonoBehaviour
{
    private bool nextEvent = false;
    [SerializeField] private GameObject deathBox = null;
    private void OnTriggerEnter(Collider other)
    {
        if (!nextEvent && other.tag == "Player")
        {
            print("test");
            deathBox.SetActive(true);
            nextEvent = true;
        }
    }
}
