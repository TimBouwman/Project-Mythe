using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceIsDone : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float audioLength;
    [SerializeField] private GameObject deathCanvas;

    private void Awake()
    { 
        audioLength = audioSource.clip.length;
    }
    // Update is called once per frame
    void Update()
    {
        audioLength -= Time.deltaTime;
        if (audioLength < 0)
        {
            audioLength = audioSource.clip.length;
            deathCanvas.SetActive(true);
            gameObject.SetActive(false);
        }



    }
}
