using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player currentPlayer;
    public Player bluePlayer;
    public Player redPlayer;
    public static GameManager gameManager = null;
    public Node pressedButton;
    public Wall selectedWall;

    public GameObject blueMoveCover;
    public GameObject blueAcionCover;
    public GameObject redMoveCover;
    public GameObject redAcionCover;

    public List<Rigidbody> rbds;
    public GameObject finishEffect;
    public Text winText;

    // Use this for initialization
    void Awake()
    {
        if (gameManager == null) gameManager = this;
        else if (gameManager != null) Destroy(gameObject);
        //buildCount = bluePlayer.buildCount;
        //bluePlayer.isBuild = true;
        currentPlayer = bluePlayer;
        bluePlayer.isMove = true;
        redPlayer.isMove = false;
        rbds = new List<Rigidbody>();
        rbds.Add(FindObjectOfType<Finish>().GetComponent<Rigidbody>());
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTurn();
        CoverControl();
    }

    public void JudgeWinner(string winnerPlayer)
    {
        winText.text = "Winner : " + winnerPlayer + "!!";
        winText.gameObject.SetActive(true);

        finishEffect.SetActive(true);
        foreach (Rigidbody rbd in rbds)
        {
            if(rbd != null) rbd.isKinematic = false;
        }

        //StartCoroutine("BlinkText");
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            Debug.Log("loop");
            Color color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
            winText.color = color;

            yield return new WaitForSeconds(0.2f);
        }
    }

    void ChangeTurn()
    {

        if (!bluePlayer.isMove && (bluePlayer.moveCount == 3))
        {
            currentPlayer = redPlayer;
            redPlayer.isMove = true;
            redPlayer.moveCount = 0;
            bluePlayer.moveCount = 0;
            redPlayer.buildCount = 0;
            bluePlayer.buildCount = 0;
            ColorReset();
        }


        else if (!redPlayer.isMove && (redPlayer.moveCount == 3))
        {
            currentPlayer = bluePlayer;
            bluePlayer.isMove = true;
            bluePlayer.moveCount = 0;
            redPlayer.moveCount = 0;
            bluePlayer.buildCount = 0;
            redPlayer.buildCount = 0;
            ColorReset();
        }
    }

    private void ColorReset()
    {
        foreach (Node node in FindObjectsOfType<Node>())
        {
            node.ColorReset();
        }

        foreach (Wall wall in FindObjectsOfType<Wall>())
        {
            wall.ColorReset();
        }
    }

    private void CoverControl()
    {
        if(currentPlayer == bluePlayer)
        {
            if (currentPlayer.buildCount < 2)
            {
                blueMoveCover.SetActive(true);
                blueAcionCover.SetActive(false);
            }
            else
            {
                blueMoveCover.SetActive(false);
                blueAcionCover.SetActive(true);
            }
        }
        else
        {
            if (currentPlayer.buildCount < 2)
            {
                redMoveCover.SetActive(true);
                redAcionCover.SetActive(false);
            }
            else
            {
                redMoveCover.SetActive(false);
                redAcionCover.SetActive(true);
            }
        }
    }
}
