using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class gridMap : MonoBehaviour
{

    public GameObject error;
    public GameObject checkmark;

    public Tilemap ground;
    public Tilemap collideable;


    // Start is called before the first frame update
    void Start()
    {

        for (int f1 = -20; f1 < 20; f1++)
        {
            for (int f2 = -20; f2 < 20; f2++)
            {

                if (collideable.GetTile(collideable.WorldToCell(new Vector3(f1,f2,0))) == null) {
                   Instantiate(checkmark, new Vector3(f1 + 0.5f, f2 + 0.5f, 0), Quaternion.identity);
              }else{
                    Instantiate(error, new Vector3(f1 + 0.5f , f2 + 0.5f, 0), Quaternion.identity);
              }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
