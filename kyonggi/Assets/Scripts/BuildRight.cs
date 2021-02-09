using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRight : MonoBehaviour
{
    private AudioSource audioSource;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  
    }

    public void OnClick()
    {
        if (gameManager.pressedButton != null)
        {
            if (gameManager.pressedButton.transform.position.x >= -0.2f || gameManager.currentPlayer.buildCount >= 2) return;
            if (gameManager.pressedButton.GetWalls()[0] == 1) return;

            gameManager.currentPlayer.IncreaseBuildCount();
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            gameManager.pressedButton.BuildWall(0);
            gameManager.pressedButton.ChangeColor();
            gameManager.pressedButton = null;
        }
    }
}
