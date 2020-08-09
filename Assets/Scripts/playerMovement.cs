using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMovement : MonoBehaviour
{
    public int illusionLifeTime = 5;

    public GameObject illusion;
    public rewindIndicator indicator;       //posibilitatea de a introduce indicatorul ca child?

    public List<Vector3> lastdir = new List<Vector3>(); //Lista care pastreaza ultimele pozitii
    public int pozitiirewind = 5;

    public RaycastHit2D hit2;
    private bool jumped = false;
    private float initialVolume;
    private GameObject instance;
    private Transform playerSprite;
    private int illusionEndTime;
    private ParticleSystem particles;
    private Tilemap cop;

    void Start()
    {
        cop = gameLogic.Instance.collideable;       //stratul de coliziune

        lastdir.Insert(0, transform.position);      //salveaza pozitia initiala in lista

        indicator.transform.position = transform.position;  //initializeaza pozitia indicatorului

        particles = transform.GetChild(1).GetComponent<ParticleSystem>();

        playerSprite = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
        //Vector3Int gridCoordinates = ground.WorldToCell(this.transform.position);
        //Debug.Log(gridCoordinates.x.ToString() + "  " + gridCoordinates.y.ToString());

       if (cop.GetTile(cop.WorldToCell(transform.position)) != null)
        {
            if (jumped == false)
            {
                soundManager.PlaySound(soundManager.Sound.jump);
                jumped = true;
            }
            playerSprite.transform.position = new Vector3(0,0.7f, 0) + transform.position;
        }
        else
        {
            jumped = false;
            playerSprite.transform.position = new Vector3(0, 0.2f, 0) + transform.position;
        }
    }

    private void Controls()
    {
        Vector3 direction = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            //  timePassed += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {

            if (gameLogic.Instance.turnsSinceStart >= illusionEndTime) {
                handleRewind();
            }
        }
        else if (Input.GetKeyDown("w"))
        {
            direction = new Vector2(0, 1);
            Move(direction);
            
        }
        else if (Input.GetKeyDown("s"))
        {
            direction = new Vector2(0, -1);
            Move(direction);
            

        }
        else if (Input.GetKeyDown("d"))
        {
            direction = new Vector2(1, 0);
            Move(direction);
            

        }
        else if (Input.GetKeyDown("a"))
        {
            direction = new Vector2(-1, 0);
            Move(direction);
            
        }
        else if (Input.GetKeyDown("r"))
        {
            soundManager.PlaySound(soundManager.Sound.restart);

            gameLogic.Instance.restart();
        }

        if (gameLogic.Instance.turnsSinceStart >= illusionEndTime)
        {
            Object.Destroy(instance);
        }


        //Mute the music if you so desire.
        if (Input.GetKeyDown("m") )
        {
            if (assetManager.instance.musicSource.volume != 0f)
            {
                assetManager.instance.musicSource.volume = 0f;
            }
            else if (assetManager.instance.musicSource.volume == 0f)
            {
                assetManager.instance.musicSource.volume = assetManager.instance.musicVolume;
            }
        }
        
    }

    private void Move(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f);

        hit2 = hit;

        if (hit) { 
            if(hit.transform.tag == "Enemy")
            {
                gameLogic.Instance.death();
            }
        }
        // If it hits something...
        if ( cop.GetTile(cop.WorldToCell(destination)) == null  )
        {
            soundManager.PlaySound(soundManager.Sound.playerWalk, this.transform.position);
            if (destination != transform.position)                                  //daca se misca
            {
                gameLogic.Instance.newTurn();       //am mutat asta aici pentru a trece turnul doar daca mutarea se executa
                transform.position += direction;    //update pozitie
                lastdir.Insert(0, transform.position);            //adauga mutarea executata in lista
                if (lastdir.Count > pozitiirewind)  
                {
                    lastdir.RemoveAt(pozitiirewind);       //elimina elementul de pe pozitia de margine
                }

                indicator.UpdateIndicatorSprite();  //update sprite-ul indicatorului
            }
        }
    }

    public void handleRewind()
    {


        if (cop.GetTile(cop.WorldToCell(lastdir[lastdir.Count - 1])) == null && lastdir[lastdir.Count - 1] != transform.position)
        {
            //Teleporting particles
            particles.Play();

            instance = Instantiate(illusion, transform.position, Quaternion.identity);
            illusionEndTime = illusionLifeTime + gameLogic.Instance.turnsSinceStart;

            //Movement and recording of positions
            Vector3 poz = lastdir[lastdir.Count - 1];
            transform.position = poz;
            lastdir.Clear();
            lastdir.Insert(0, poz);
            indicator.UpdateIndicatorSprite();  //update sprite-ul indicatorului

            //Teleporting sound
            soundManager.PlaySound(soundManager.Sound.teleport, this.transform.position);

            gameLogic.Instance.newTurn();
        }
    }
}

