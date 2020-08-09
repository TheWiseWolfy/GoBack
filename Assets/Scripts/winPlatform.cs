using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public Loader.enumScene nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            soundManager.PlaySound(soundManager.Sound.finish);
            Loader.Load(nextLevel);
        }
        
    }
}
