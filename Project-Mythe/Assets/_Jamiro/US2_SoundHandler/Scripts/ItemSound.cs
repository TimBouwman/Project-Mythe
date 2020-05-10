using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    [SerializeField] [Tooltip("geluidje wat het object moet maken als hij iets raakt")] private AudioClip objectHitSomethingSound;
    private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 3) PlayItemHitSomethingSound(collision.relativeVelocity.magnitude);
    }

    private void PlayItemHitSomethingSound(float mag)
    {
        GameObject sound = ObjectPooler.SharedInstance.GetPooledObject();
        audioSource = sound.GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.transform.position = transform.position;
            audioSource.volume = (mag / 100) / 1.5f;
            audioSource.clip = objectHitSomethingSound;
            sound.SetActive(true);
        }
    }
}
