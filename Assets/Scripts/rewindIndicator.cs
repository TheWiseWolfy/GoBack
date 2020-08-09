using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class rewindIndicator : MonoBehaviour
{
    //aceasta clasa exista cu scopul de a centraliza toate sprite-urile necesare pentru indicator
 
    public playerMovement Player;
    public Sprite UP, DOWN, LEFT, RIGHT, NORMAL;       //sprite-urilie directionale
    UnityEngine.Vector3 l = new UnityEngine.Vector3(1, 0);      //referintele pentru directie
    UnityEngine.Vector3 r = new UnityEngine.Vector3(-1, 0);
    UnityEngine.Vector3 u = new UnityEngine.Vector3(0, -1);
    UnityEngine.Vector3 d = new UnityEngine.Vector3(0, 1);


    public void UpdateIndicatorSprite()     //update-ul sprite-ului indicatorului(apelat dupa orice mutare sau modificare a listei de pozitii "lastdir")
    {
        transform.position = Player.lastdir[Player.lastdir.Count - 1];
        if(Player.lastdir.Count < Player.pozitiirewind)           //daca este doar o mutare in lista reseteaza la sprite-ul default
         {
            this.GetComponentInChildren<SpriteRenderer>().sprite = NORMAL;
         } 
        else if(Player.lastdir.Count >= Player.pozitiirewind)
        {
            UnityEngine.Vector3 a, b;
            a = Player.lastdir[Player.lastdir.Count - 1];
            b = Player.lastdir[Player.lastdir.Count - 2];
            UnityEngine.Vector3 c = a - b;      //calculul directiei
            if (c == l)         //situatiile directiei si sprite
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = LEFT;        //situatie STANGA
            } else if(c == r)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = RIGHT;      //situatie DREAPTA
            } else if(c == u)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = UP;         //situatie SUS
            }
            else if (c == d)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = DOWN;      //situatie JOS
            }
        }
               
           
      
    }
}
