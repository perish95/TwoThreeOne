using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnClick()
    {
        if (FindObjectOfType<GameManager>().selectedWall != null && gameManager.currentPlayer.buildCount < 2)
        {
            gameManager.currentPlayer.IncreaseBuildCount();
            FindObjectOfType<GameManager>().selectedWall.DestroyWall();
            gameManager.selectedWall.ChangeColor();
            gameManager.selectedWall = null;
        }
    }
}
