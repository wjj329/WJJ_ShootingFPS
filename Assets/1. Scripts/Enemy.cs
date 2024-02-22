using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState // 적 상태 목록
    {
        Idle,
        Walk,
        Attack,
        Damaged,
        Dead,
    }


    public float hp = 100; //적 체력
    public Slider hpBar; // 적 체력바

    // 상태를 담아둘 변수를 만들고, 기본상태로 시작
    public EnemyState eState = EnemyState.Idle;

    Transform player; // 플레이어
    float distance; // 플레이어와의 거리



    void Damaged(float damage)
    {
        hp -= damage; // 공격받은 데미지만큼 체력 감소

        // 감소한 체력을 체력바에 표시
        hpBar.value = hp;

        if (hp > 0) // 체력이 남아있다면
        {
            eState = EnemyState.Damaged; // 피격 상태로 전환
        }
        else // 체력이 남아있지 않다면
        {
            eState = EnemyState.Dead;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // Player 컴포넌트로 찾은 플레이어의 transtorm 컴포넌트 가져오기
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 적과 플레이어 사이 거리 계산
        distance = Vector3.Distance(transform.position, player.position);

        // 기본, 이동, 공격 상태일 때 할 일 나누기
        switch (eState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Walk:
                Walk();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }



    private void Idle() // 기본 상태
    {
        if (distance <= 8) // 플레이어와의 거리가 8 이상이면
        {
            eState = EnemyState.Walk; // 이동 상태로 전환
        }
    }

    private void Walk()
    {
        if (distance > 8) // 플레이어와의 거리가 8 보다 크다면
        {
            eState = EnemyState.Idle; // 기본 상태로 전환
        }

        if (distance <= 2) // 플레이어와의 거리가 2 이하라면
        {
            eState = EnemyState.Attack; // 공격으로 전환
        }
    }

    private void Attack()
    {
        // 플레이어와의 거리가 2보다 크다면
        if (distance > 2)
        {
            eState = EnemyState.Walk; // 이동 상태로 전환
        }
    }
}
