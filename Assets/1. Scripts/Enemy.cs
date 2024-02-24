using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI; // AI 관련 클래스 사용

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

    NavMeshAgent agent; // NavMeshAgent 컴포넌트



    void Damaged(float damage)
    {
        hp -= damage; // 공격받은 데미지만큼 체력 감소

        // 감소한 체력을 체력바에 표시
        hpBar.value = hp;

        agent.isStopped = true;// 이동중단
        agent.ResetPath(); // 경로 초기화

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

        agent = GetComponent<NavMeshAgent>(); // Nav mesh Agent 컴포넌트 가져옴
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

            agent.isStopped = false; // 이동 시작
        }
    }

    private void Walk()
    {
        if (distance > 8) // 플레이어와의 거리가 8 보다 크다면
        {
            eState = EnemyState.Idle; // 기본 상태로 전환
            agent.isStopped = true; // 이동 중단
            agent.ResetPath(); // 경로 초기화
        }

        else if (distance <= 2) // 플레이어와의 거리가 2 이하라면
        {
            eState = EnemyState.Attack; // 공격으로 전환
            agent.isStopped = true; // 이동 중단
            agent.ResetPath(); // 경로 초기화
        }

        // 그 외 상황 : 다른 상태로 전환하지 않을 때
        else
        {
            // 플레이어 위치를 목적지로 설정
            agent.SetDestination(player.position);
        }
    }

    private void Attack()
    {
        // 플레이어와의 거리가 2보다 크다면
        if (distance > 2)
        {
            eState = EnemyState.Walk; // 이동 상태로 전환
            agent.isStopped = false; // 이동 시작
        }
    }
}
