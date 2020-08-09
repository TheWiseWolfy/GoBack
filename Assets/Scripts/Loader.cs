using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum enumScene{
        SampleScene,
        TEST,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
        Level13,
        INTRO,
    }

    public static void Load(enumScene scene){
        SceneManager.LoadScene(scene.ToString());

    }

}
