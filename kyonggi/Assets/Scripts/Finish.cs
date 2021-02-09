using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {
    private bool switchThis;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !switchThis)
        {
            FindObjectOfType<GameManager>().JudgeWinner(collision.gameObject.GetComponent<Player>().name);
            switchThis = true;
        }
    }
}
