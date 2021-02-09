using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 10f;
    public bool isMove;
    public int moveCount;
    public int buildCount = 0;
    public GameObject btns;

    public AudioSource audioSource;
    public AudioClip moveClip;
    public AudioClip rotateClip;

    private GameManager gameManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount == 3)
        {
            gameManager.pressedButton = null;
            gameManager.selectedWall = null;
            isMove = false;
        }
        if (!isMove) btns.SetActive(false);
        else btns.SetActive(true);
    }

    public void MoveFunc()
    {
        if (buildCount < 2) return;

        Vector3 temp;
        Vector3 movePos = new Vector3(0, 0, 1.16f);
        if (transform.rotation == Quaternion.Euler(0, 270, 0) || transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            Debug.Log("west");
            temp = new Vector3(-1.16f, 0, 0);
            if (!Physics.Linecast(transform.position, transform.position + temp))
            {
                Moving(movePos);
            }
        }

        else if (transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            Debug.Log("east");
            temp = new Vector3(1.16f, 0, 0);
            if (!Physics.Linecast(transform.position, transform.position + temp))
            {
                Moving(movePos);
            }
        }

        else if (transform.rotation == Quaternion.identity || transform.rotation == Quaternion.Euler(0,360,0))
        {
            Debug.Log("north");
            temp = new Vector3(0, 0, 1.16f);
            if (!Physics.Linecast(transform.position, transform.position + temp))
            {
                Moving(movePos);
            }
        }

        else if (transform.rotation == Quaternion.Euler(0, 180, 0))
        {
            Debug.Log("south");
            temp = new Vector3(0, 0, -1.16f);
            if (!Physics.Linecast(transform.position, transform.position + temp))
            {
                Moving(movePos);
            }
        }
    }

    public void TurnRight()
    {
        if (buildCount < 2) return;

        Quaternion right = Quaternion.identity;
        right.eulerAngles = transform.eulerAngles + new Vector3(0f, 90, 0f);
        //transform.Rotate(new Vector3(0, 90, 0));
        transform.rotation = right;
        Rotating();
    }

    public void TurnLeft()
    {
        if (buildCount < 2) return;

        Quaternion left = Quaternion.identity;
        left.eulerAngles = transform.eulerAngles + new Vector3(0f, -90, 0f);
        //transform.Rotate(new Vector3(0, 270, 0));
        transform.rotation = left;
        Rotating();

    }

    private void Moving(Vector3 movePos)
    {
        audioSource.clip = moveClip;
        audioSource.Play();
        transform.Translate(movePos);
        moveCount++;
    }

    private void Rotating()
    {
        audioSource.clip = rotateClip;
        audioSource.Play();
        moveCount++;
    }

    public void IncreaseBuildCount()
    {
        if(buildCount<2) buildCount++;
    }
}
