using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonTrigger : MonoBehaviour
{
    public event EventHandler<EventArgs> OnButtonReleased;
    public event EventHandler<EventArgs> OnButtonPressed;

    private List<GameObject> inTrigger = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!this.inTrigger.Contains(collision.gameObject))
            {
                this.inTrigger.Add(collision.gameObject);
            }
            if (this.inTrigger.Count > 0)
            {
                this.OnButtonPressed?.Invoke(this, new EventArgs());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.inTrigger.Contains(collision.gameObject))
            {
                this.inTrigger.Remove(collision.gameObject);
            }
            if (this.inTrigger.Count == 0)
            {
                this.OnButtonReleased?.Invoke(this, new EventArgs());
            }
        }
    }
}
