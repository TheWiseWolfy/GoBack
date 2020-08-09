using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class assetManager : MonoBehaviour
{
    private static assetManager _instance = null;

    //In case the class is needed but there's no instance yet, we will make one.
    public static assetManager instance
    {
        get
        {

            if (_instance == null)
            {

                _instance = Instantiate(Resources.Load<assetManager>("AssetManager"));
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }

    }

    //Array for all audio clips
    [Header("Audio")]
    [Range(0, 1)]
    public float musicVolume;
    public SoundAudioClip[] soundAudioClipArray;

    public AudioSource musicSource;

    private void Awake()
    {
        musicSource = transform.GetComponent<AudioSource>();
        musicSource.volume = musicVolume;

    }


    //Classes
    [System.Serializable]
    public class SoundAudioClip
    {
        public soundManager.Sound sound;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float Volume;
    }

}
