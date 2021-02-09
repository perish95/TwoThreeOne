using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildNorth : MonoBehaviour
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
            if (gameManager.pressedButton.transform.position.z >= 11 || gameManager.currentPlayer.buildCount >= 2) return;
            if (gameManager.pressedButton.GetWalls()[1] == 1)
            {
                Debug.Log("north");
                return;
            }

            gameManager.currentPlayer.IncreaseBuildCount();
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            gameManager.pressedButton.BuildWall(1);
            gameManager.pressedButton .ChangeColor();
            gameManager.pressedButton = null;
        }
    }
}
