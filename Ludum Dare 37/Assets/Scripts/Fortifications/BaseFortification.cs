using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFortification : MonoBehaviour {

    [SerializeField]
    private int _health = 10;

    private bool _isPreview = false;
    const float PREVIEW_ALPHA = 0.3f;

    public void SetPreview()
    {
        _isPreview = true;

        SetSpritesPreviewAlpha();
    }

    private void SetSpritesPreviewAlpha()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            Color tmp = sprite.color;
            tmp.a = PREVIEW_ALPHA;
            sprite.color = tmp;
        }
    }

    protected bool CanUpdate()
    {
        return !_isPreview;
    }

    virtual public bool IsAttackable()
    {
        return !_isPreview;
    }

    public bool TakeDamage(int damage)
    {
        _health -= damage;
        return (_health <= 0);
    }

    virtual public void PlayAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
    }
}
