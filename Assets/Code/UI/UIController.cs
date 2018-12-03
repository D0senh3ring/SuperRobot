using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Vector2 respawnSlotCornerOffset;
    [SerializeField]
    private RespawnSlot respawnSlotPrefab;

    private GameController gameController;
    private RespawnSlot[] respawnSlots;

    private void Start()
    {
        this.gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        this.InitializeRespawnSlots();
        this.gameController.OnRespawnCountChanged += this.RefreshRespawnDisplay;
    }

    private void OnDestroy()
    {
        this.gameController.OnRespawnCountChanged -= this.RefreshRespawnDisplay;
    }

    private void RefreshRespawnDisplay(object sender, RespawnCountChangedEventArgs e)
    {
        for(int i = e.RemainingRespawns; i < e.MaxRespawns; i++)
        {
            this.respawnSlots[i].SetPlayerSpriteVisible(false);
        }
    }

    private void InitializeRespawnSlots()
    {
        if (this.respawnSlots == null)
        {
            this.respawnSlots = new RespawnSlot[this.gameController.MaximumRespawns];

            for(int i = 0; i < this.respawnSlots.Length; i++)
            {
                RespawnSlot current = this.respawnSlots[i] = GameObject.Instantiate(this.respawnSlotPrefab, this.transform);

                RectTransform rect = current.GetComponent<RectTransform>();
                rect.anchoredPosition = this.respawnSlotCornerOffset + new Vector2(rect.sizeDelta.x * i, 0);
            }
        }
    }
}
