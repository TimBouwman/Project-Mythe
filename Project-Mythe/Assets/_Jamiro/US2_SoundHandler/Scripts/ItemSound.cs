using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    #region Variables
    //variable voor audio objecten die gepooled word
    [SerializeField] [Tooltip("geluidje wat het object moet maken als hij iets raakt")] private AudioClip objectHitSomethingSound = null;
    [SerializeField] [Tooltip("de magnitude van de velocity / 100  en dan nog eens delen door dit")] private float soundDevideBy = 1.25f;
    private AudioSource objectPoolingAudioSource;
    //variable voor het script zelf
    private float mag;
    [SerializeField] private float minRelativeVelocity = 3;
    // variable voor audio wat van dit object komt;
    [SerializeField] [Tooltip("geluidje wat het object moet maken wanneer je het object gebruikt")] private AudioClip objectSoundWhenUsed = null;
    [SerializeField] [Tooltip("de audiosource die op dit item moet")] private AudioSource thisObjectAudioSource;
    [SerializeField] [Tooltip("3d of 2d geluid voor dit item. 1 tot 0")] private float ThisItemIsUsedAudioDimension = 1;
    [SerializeField] [Tooltip("minimale distance van het object om het geluid te horen")] private float ThisItemIsUsedAudioMinDistance = 0;
    [SerializeField] [Tooltip("maximale distance van het object om het geluid te horen")] private float ThisItemIsUsedAudioMaxDistance = 5;

    #endregion

    #region Unity methods

    private void Start() { if (thisObjectAudioSource != null) thisObjectAudioSource = GetComponent<AudioSource>(); }

    // checkt voor collision met iets en als het hard genoeg is speeld ie geluid af
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= minRelativeVelocity)
        {
            mag = collision.relativeVelocity.magnitude;
            PoolItemSound();
            AIListenToSound();
        }
    }
    #endregion

    #region Sound methods
    
    private void AIListenToSound()
    {
        AIMoveToSound.playSound(transform.position);
    }

    private void SetAudioSettingForThisObject()
    {
        thisObjectAudioSource.clip = objectSoundWhenUsed;
        thisObjectAudioSource.spatialBlend = ThisItemIsUsedAudioDimension;
        thisObjectAudioSource.minDistance = ThisItemIsUsedAudioMinDistance;
        thisObjectAudioSource.maxDistance = ThisItemIsUsedAudioMaxDistance;
        thisObjectAudioSource.playOnAwake = false;
        thisObjectAudioSource.loop = false;
    }
    

    //vraagt van de objectpooler een audio object uit de pool aan 
    
    private void PoolItemSound()
    {
        GameObject sound = ObjectPooler.SharedInstance.GetPooledObject();
        objectPoolingAudioSource = sound.GetComponent<AudioSource>();
        SetHitSomethingSoundSettings(sound);
    }
    


    // hoe hard het geluid zal zijn word bepaald en de goeie audio clip word mee gegeven.
    private void SetHitSomethingSoundSettings(GameObject sound)
    {
            objectPoolingAudioSource.volume = (mag / 100) / soundDevideBy;
            objectPoolingAudioSource.clip = objectHitSomethingSound;
            sound.transform.position = transform.position;
            sound.SetActive(true);
    }

    // wanneer je een item gebruikt deze aanroepen
    // speelt het geluid af wanneer een item gebruikt word.
    private void ItemIsUsedSound()
    {
        thisObjectAudioSource.Play();
        AIListenToSound();
    }
    #endregion
}
