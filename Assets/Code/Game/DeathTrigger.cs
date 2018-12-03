using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathTrigger : MonoBehaviour
{
    private GameController gameController;

    private PlayerController lastPlayerEntered;

    private void Start()
    {
        this.gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(this.gameController.CurrentPlayer.Equals(collision.gameObject.transform))
            {
                this.lastPlayerEntered = collision.GetComponent<PlayerController>();

                this.lastPlayerEntered.Sacrifice(RespawnReason.FellIntoVoid);
                this.StartCoroutine("FreezePlayerPositionLater");
            }
        }
    }

    private IEnumerator FreezePlayerPositionLater()
    {
        PlayerController current = this.lastPlayerEntered;
        this.lastPlayerEntered = null;

        yield return new WaitForSeconds(4.0f);

        current.RigidBody2D.bodyType = RigidbodyType2D.Static;
    }
}
