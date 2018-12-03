using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        this.IsSacrificeDown = Input.GetButtonDown("Sacrifice");
        this.HorizontalMoveAxis = Input.GetAxis("Horizontal");
        this.IsJumpPressed = Input.GetButton("Jump");
        this.IsRunPressed = Input.GetButton("Run");

        if (Input.GetButtonDown("Jump"))
        {
            this.IsJumpDown = true;
        }
    }

    public float HorizontalMoveAxis { get; private set; }
    public bool IsSacrificeDown { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsRunPressed { get; private set; }
    public bool IsJumpDown { get; set; }
}
