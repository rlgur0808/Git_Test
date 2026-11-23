using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    //데미지를 받을 수 있는 녀석
    //체력, 최대체력
    //죽으면 얼마나 점수를 부여할지

    [SerializeField]
    private float hp = 0f;
    private float maxHp = 100f;
    private int score = 10;

    [SerializeField]
    private float moveSpeed = 3f;

    private Player player = null;

    private Action<Enemy, int> onEnemyDie = null;

    private AudioSource source = null;

    private void Awake()
    {
        hp = maxHp;
        source = GetComponent<AudioSource>();
    }

    public void Init(Player player, Action<Enemy, int> onEnemyDie)
    {
        this.player = player;
        this.onEnemyDie = onEnemyDie;
        StartCoroutine(CoChasePlayer());
    }

    private IEnumerator CoChasePlayer()
    {
        while (true)
        {
            //플레이어의 위치 쫓아가는 로직
            Vector3 direction =
                (player.transform.position
                - transform.position).normalized;

            float distance =
                (player.transform.position
                - transform.position).magnitude;

            if (distance >= 3f)
            {
                transform.position += direction
                    * moveSpeed * Time.deltaTime;
            }
            else
            {
                player.TakeDamage(10f);
                yield return new WaitForSeconds(2f);
            }
            yield return null;
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        source.Play();

        if (hp <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        onEnemyDie?.Invoke(this, score);

        Destroy(gameObject);
    }


}
