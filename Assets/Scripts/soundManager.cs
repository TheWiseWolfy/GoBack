using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class soundManager 
{
    //Enum of sounds curently implemented
    public enum Sound
    {
        playerWalk,
        teleport,
        restart,
        finish,
        button,
        wrong,
        jump,
    }



    //We check the asset manager array and see if there's any clip associated.
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (assetManager.SoundAudioClip soundAudioClip in assetManager.instance.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found!");   //We did not find any clip with the required sound
        return null;
    }
    private static float GetAudioVolume(Sound sound)
    {
        foreach (assetManager.SoundAudioClip soundAudioClip in assetManager.instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.Volume;
            }
        }
        return 1;
    }
    //We play a sound in game space using a position
    public static void PlaySound(Sound sound,Vector3 position)
    {
        //We create a new game object and move it in place
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;

        Object.DontDestroyOnLoad(soundGameObject);

        //We create an audio souce component and load the propper clip
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);

        //Settings
        audioSource.volume = GetAudioVolume(sound);
        audioSource.maxDistance = 100f;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = 0f;
        audioSource.Play();

        //We destroy the object after the clip is done.
        Object.Destroy(soundGameObject, audioSource.clip.length);
     }


    //We play a sound without posiztion
    public static void PlaySound(Sound sound)
    {
        //Analog
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        Object.DontDestroyOnLoad(soundGameObject);

        audioSource.volume = GetAudioVolume(sound);
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();  //

        Object.Destroy(soundGameObject, audioSource.clip.length);

    }
}
