using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private ObjectPool<Bullet> bulletObjectPool;

    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private Transform bulletsTransform;

    private Coroutine spawnBulletCoroutine;
    private float spawnCoolTime = 2.0f;

    public static BulletManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        bulletObjectPool = new ObjectPool<Bullet>(bulletPrefab, 20);
    }

    public void StartSpawnBullet()
    {
        spawnBulletCoroutine = StartCoroutine(AsyncSpawnBullet());
    }

    private IEnumerator AsyncSpawnBullet()
    {
        float curTime = 0;
        while (true)
        {
            yield return null;
            curTime += Time.deltaTime;
            if(curTime >= spawnCoolTime)
            {
                Bullet bullet = bulletObjectPool.GetObject();
                bullet.transform.SetParent(bulletsTransform);
                bullet.SetSize(new Vector2(Screen.width / (StageManager.STAGE_WIDTH_BLOCK_COUNT * 16), Screen.height / (StageManager.STAGE_HEIGHT_BLOCK_COUNT * 8)));
                bullet.SetAttackStat(100);
                bullet.SetHp(15);
                Vector2 dir;
                do
                {
                    dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                } while (dir == Vector2.zero);
                dir.Normalize();
                bullet.SetDir(dir);
                bullet.SetMoveSpeed(100);
                bullet.Shoot();
                curTime = 0;
            }
        }
    }

    public void ReturnObject(Bullet bullet)
    {
        bulletObjectPool.ReturnObject(bullet);
    }

}