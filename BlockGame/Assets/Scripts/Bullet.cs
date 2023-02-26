using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
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
            if (hp <= 0)
            {
                Explode();
            }
        }
    }
    private float attackStat;
    private float moveSpeed;
    private Vector2 dir;

    private RectTransform rectTransform;
    private BoxCollider2D boxCollider;

    private Coroutine moveCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetAttackStat(float attackStat)
    {
        this.attackStat = attackStat;
    }

    public void SetHp(float hp)
    {
        Hp = hp;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void SetDir(Vector2 dir)
    {
        this.dir = dir;
    }

    public void Shoot()
    {
        rectTransform.anchoredPosition = Vector2.zero;
        moveCoroutine = StartCoroutine(AsyncMove());
    }

    private IEnumerator AsyncMove()
    {
        while (true)
        {
            yield return null;
            rectTransform.anchoredPosition += moveSpeed * Time.deltaTime * dir;
        }
    }

    private void Explode()
    {
        boxCollider.enabled = false;
        rectTransform.DOScale(1.5f, 0.5f).OnComplete(() =>
        {
            BulletManager.instance.ReturnObject(this);
            boxCollider.enabled = true;
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            dir *= -1;
            collision.transform.GetComponent<Block>().TakeDamage(attackStat);
            Hp -= 5;
        }
    }
}