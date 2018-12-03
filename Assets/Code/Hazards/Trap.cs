using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trap : MonoBehaviour {

    private GameController gameController;

    private void Start()
    {
        this.gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.Equals(this.gameController.CurrentPlayer.gameObject))
        {
            collision.gameObject.GetComponent<PlayerController>().Sacrifice(RespawnReason.TouchedHazard);
        }
    }
}
