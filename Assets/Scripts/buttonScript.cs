using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class buttonScript : MonoBehaviour
{
    private GameObject spriteChild;
    private SpriteRenderer Sprite;

    public Sprite buttonPressed;
    public Sprite buttonUnpressed;
    public Sprite buttonPressedToggle;
    public Sprite buttonUnpressedToggle;

    public bool toggle;
    public bool pressed;

    private void Start()
    {
        spriteChild = this.transform.GetChild(0).gameObject;
        Sprite = spriteChild.GetComponent<SpriteRenderer>();
        
        if (toggle == true) Sprite.sprite = buttonUnpressedToggle;
        else Sprite.sprite = buttonUnpressed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (toggle == true && pressed == false || toggle ==false)
                soundManager.PlaySound(soundManager.Sound.button);
        }

        pressed = true;

        if (toggle == true) Sprite.sprite = Sprite.sprite = buttonPressedToggle;
        else Sprite.sprite = Sprite.sprite = buttonPressed;


    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (toggle == false) 
        { 
        pressed = false;
        Sprite.sprite = buttonUnpressed;
        }
    }
}
