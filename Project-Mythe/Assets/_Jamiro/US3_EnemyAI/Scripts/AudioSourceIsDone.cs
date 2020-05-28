using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceIsDone : MonoBehaviour
{
    private AudioSource audioSource;
    private float audioLength;
    [SerializeField] private GameObject deathCanvas;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
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
