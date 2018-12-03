using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FinishLine : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        this.gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && this.gameController.CurrentPlayer.Equals(collision.gameObject.transform))
        {
            this.gameController.LoadNextLevel();
        }
    }
}
