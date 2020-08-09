using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class controllerButton : MonoBehaviour
{
    public buttonScript[] button;
    public TileBase doorSprite;
    public Tilemap Colidable;
    public bool startopen = false;

    private Transform locations = null;

    // Start is called before the first frame update
    void Start()
    {   
        
        locations = transform.GetChild(0);
        if (startopen == true)
        {
            doorOpen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool valid = true;

        foreach (buttonScript b in button)
        {
           if(b.pressed == false)
            {
                valid = false;
            }
        }


        if (valid)
        {
            if (startopen == false)
                doorOpen();
            else doorClose();
        }
        else
        {
            if (startopen == false)
                doorClose();
            else doorOpen();
        }
    }

    private void doorClose()
    {

        foreach (Transform child in locations)
        {
            Vector3Int location = Colidable.WorldToCell(child.position);
            Colidable.SetTile(location, doorSprite);
        }
    }
    private void doorOpen()
    {

        foreach (Transform child in locations)
        {
            Vector3Int location = Colidable.WorldToCell(child.position);
            Colidable.SetTile(location, null);
        }
    }
}
