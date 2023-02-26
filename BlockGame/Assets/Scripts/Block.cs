using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float hp;
    private float Hp
    {
        get
        {
            return Hp;
        }
        set
        {
            hp = value;
            if(hp < 0)
            {

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
        boxCollider.offset = new Vector2(size.x / 2, -size.y / 2);
        boxCollider.size = size;
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    public void TakeDamage(float damage)
    {
        Hp = Mathf.Max(0, Hp - damage);
    }

    private void Explode()
    {
        StageManager.instance.ReturnObject(this);
    }
}
