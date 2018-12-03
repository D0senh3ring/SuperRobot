using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float minXCoordinate;
    [SerializeField]
    private Vector2 followOffset;
    [SerializeField]
    private float smoothTime = 0.1f;

    private GameController gameController;
    private Vector3 currentVelocity;
    private Transform cameraTarget;

    private void Awake()
    {
        this.gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        this.gameController.OnCurrentPlayerChanged += this.CurrentPlayerChanged;
    }

    private void OnDestroy()
    {
        this.gameController.OnCurrentPlayerChanged -= this.CurrentPlayerChanged;
    }

    private void Update()
    {
        if(this.cameraTarget != null)
        {
            Vector3 targetPosition = this.cameraTarget.position;
            targetPosition.z = this.transform.position.z;
            targetPosition.x += this.followOffset.x;
            targetPosition.y += this.followOffset.y;

            if(targetPosition.x < this.minXCoordinate)
            {
                targetPosition.x = this.minXCoordinate;
            }

            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref currentVelocity, this.smoothTime);
        }
    }

    private void CurrentPlayerChanged(object sender, PlayerChangedEventArgs e)
    {
        this.cameraTarget = e.NewPlayer;
    }
}
