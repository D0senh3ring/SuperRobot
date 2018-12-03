using UnityEngine;
using System;

public class PlayerChangedEventArgs : EventArgs
{
    public PlayerChangedEventArgs(Transform newPlayer)
    {
        this.NewPlayer = newPlayer;
    }

    public Transform NewPlayer { get; private set; }
}
