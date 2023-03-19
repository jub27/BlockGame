using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    private float hp;
    private float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if(hp <= 0)
            {
                Explode();
            }
        }
    }
    private RectTransform rectTransform;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetHp(float hp)
    {
        this.Hp = hp;
    }

    public void SetSize(Vector2 size)
    {
        rectTransform.sizeDelta = size;
        boxCollider.size = size;
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.localPosition = position;
    }

    public void TakeDamage(float damage)
    {
        Hp = Mathf.Max(0, Hp - damage);
    }

    private void Explode()
    {
        boxCollider.enabled = false;
        rectTransform.DOScale(1.5f, 0.5f).OnComplete(() =>
        {
            StageManager.instance.ReturnObject(this);
            boxCollider.enabled = true;
        });
    }
}
