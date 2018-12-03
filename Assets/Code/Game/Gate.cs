using UnityEngine;
using System;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private ButtonTrigger buttonTrigger;
    [SerializeField]
    private Transform openTarget;
    [SerializeField]
    private float smoothTime;

    private Vector3 currentTargetPosition;
    private Vector3 closeTarget;

    private void Start()
    {
        this.closeTarget = this.transform.localPosition;
        this.currentTargetPosition = this.closeTarget;

        this.buttonTrigger.OnButtonReleased += this.ButtonReleased;
        this.buttonTrigger.OnButtonPressed += this.ButtonPressed;
    }

    private void OnDestroy()
    {
        this.buttonTrigger.OnButtonReleased -= this.ButtonReleased;
        this.buttonTrigger.OnButtonPressed -= this.ButtonPressed;
    }

    private void Update()
    {
        if(this.transform.localPosition != this.currentTargetPosition)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.currentTargetPosition, this.smoothTime * Time.deltaTime);
        }
    }

    private void ButtonPressed(object sender, EventArgs e)
    {
        this.currentTargetPosition = this.openTarget.localPosition;
    }

    private void ButtonReleased(object sender, EventArgs e)
    {
        this.currentTargetPosition = this.closeTarget;
    }
}
