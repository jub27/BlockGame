using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float attackStat;
    private float moveSpeed;
    private Vector2 dir;
    private RectTransform rectTransform;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetAttackStat(float attackStat)
    {
        this.attackStat = attackStat;
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
        dir = Random.rotation.eulerAngles.normalized;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Block"))
        {
            dir *= -1;
            collision.transform.GetComponent<Block>().TakeDamage(attackStat);
        }
    }
}