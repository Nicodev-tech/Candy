using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField] private AudioSource soundFXObject;
    void Awake()
    {
        if(Instance == null) { Instance = this;}
    }

    
public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    //spawn in gameObject
    { 
    AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        //assign the audioClip
    audioSource.clip = audioClip;
    //assign volume
    audioSource.volume = volume;
    //play sound
    audioSource.Play();
    //get length of sound FX clip
    float clipLength = audioSource.clip.length;
    //destroy the clip after it is done playing
    Destroy(audioSource.gameObject, clipLength);
    }
 public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    //spawn in gameObject
    {
        int rand = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        //assign the audioClip
        audioSource.clip = audioClip[rand];
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of sound FX clip
        float clipLength = audioSource.clip.length;
        //destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
