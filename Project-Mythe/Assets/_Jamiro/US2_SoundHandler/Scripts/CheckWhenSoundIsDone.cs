using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWhenSoundIsDone : MonoBehaviour
{
    private AudioSource audioSource;
    private float audioLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioLength = audioSource.clip.length;
        print(audioSource.clip.length);
    }
    // Update is called once per frame
    void Update()
    {
        audioLength -= Time.deltaTime;
        if (audioLength < 0)
        {
            audioLength = audioSource.clip.length;
            gameObject.SetActive(false);
            
        }
       
    }
    
}
