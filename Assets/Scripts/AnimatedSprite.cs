using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animTime = 0.25f;
    public int animFrame { get; private set; }
    public bool loop = true;
    // Start is called before the first frame update
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animTime, this.animTime);
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled) 
        { 
            return;
        }


        this.animFrame++;
        if (this.animFrame >= this.sprites.Length && this.loop)
        {
            this.animFrame = 0;
        }

        if (this.animFrame >= 0 && this.animFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animFrame];
        }
    }

    public void RestartAnim()
    {
        this.animFrame = -1;

        Advance();
    }
}
