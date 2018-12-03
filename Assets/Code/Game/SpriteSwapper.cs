using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwapper : MonoBehaviour
{
    [SerializeField]
    private Sprite primarySprite;
    [SerializeField]
    private Sprite secondarySprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.SwapSprite(true);
    }

    public void SwapSprite(bool toPrimary)
    {
        this.spriteRenderer.sprite = toPrimary ? this.primarySprite : this.secondarySprite;
    }
}
