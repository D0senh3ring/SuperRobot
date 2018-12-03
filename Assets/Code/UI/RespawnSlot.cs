using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RespawnSlot : MonoBehaviour
{
    [SerializeField]
    private Image playerSprite;

    public void SetPlayerSpriteVisible(bool visible)
    {
        this.playerSprite.enabled = visible;
    }
}
