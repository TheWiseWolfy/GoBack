using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class enemyMovement : MonoBehaviour
{

    private Tilemap cop;

    private BoxCollider2D collider;

    public bool isPlayer = false;

    Vector3 mutaredubla, mutaresingulara;

    public enum Type
    {
        Left,
        Right,
        Up,
        Down
    }

    public Type enemyType;

    void Start()
    {
        cop = gameLogic.Instance.collideable;       //stratul de coliziune


        collider = transform.GetComponent<BoxCollider2D>();  //coliziune

        if (enemyType == Type.Right)
        {
            collider.offset = new Vector2(-0.5f, 0);
            collider.size = new Vector2(2, 1);
            mutaredubla = new Vector3(2, 0);
            mutaresingulara = new Vector3(1, 0);

        }
        else if (enemyType == Type.Left)
        {
            collider.offset = new Vector2(0.5f, 0);
            collider.size = new Vector2(2, 1);
            mutaredubla = new Vector3(-2, 0);
            mutaresingulara = new Vector3(-1, 0);

        }
        else if (enemyType == Type.Up)
        {
            collider.offset = new Vector2(0, -0.5f);
            collider.size = new Vector2(1, 2);
            mutaredubla = new Vector3(0, 2);
            mutaresingulara = new Vector3(0, 1);

        }
        else if (enemyType == Type.Down)
        {
           collider.offset = new Vector2(0, 0.5f);
            collider.size = new Vector2(1, 2);
            mutaredubla = new Vector3(0, -2);
            mutaresingulara = new Vector3(0, -1);

        }

        gameLogic.Instance.onNewTurn += doMove;     //event onNewTurn
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gameLogic.Instance.death();
        }
        
    }

    void doMove()
    {

        Vector3 destination = transform.position;

        if (cop.GetTile(cop.WorldToCell(destination + mutaresingulara)) == null)
        {
            if (cop.GetTile(cop.WorldToCell(destination + mutaredubla)) == null)
            {
                transform.position += mutaredubla;
                if (cop.GetTile(cop.WorldToCell(destination + mutaresingulara)) != null)
                {
                    stop();
                }
            }
            else
            {
                transform.position += mutaresingulara;
                stop();
            }

        }
        else
        {
            stop();
        }





        
    }

    void stop()
    {
        gameLogic.Instance.onNewTurn -= doMove;
        collider.size = new Vector2(1, 1);
        collider.offset = new Vector2(0, 0);
    }
}
