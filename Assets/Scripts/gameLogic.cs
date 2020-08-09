using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class gameLogic : MonoBehaviour
{
    public static gameLogic Instance { set; get; }

    public int turnsSinceStart = 0;

    public Tilemap ground;
    public Tilemap collideable;

    private void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(Instance.gameObject);
    }

    public event Action onNewTurn;

    public void newTurn()
    {
        if (onNewTurn != null)
        {
            onNewTurn();
        }
        turnsSinceStart++;
    }

    public void restart()
    {
        soundManager.PlaySound(soundManager.Sound.restart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void death()
    {
        soundManager.PlaySound(soundManager.Sound.wrong);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
