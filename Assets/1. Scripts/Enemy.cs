using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI; // AI ���� Ŭ���� ���

public class Enemy : MonoBehaviour
{
    public enum EnemyState // �� ���� ���
    {
        Idle,
        Walk,
        Attack,
        Damaged,
        Dead,
    }


    public float hp = 100; //�� ü��
    public Slider hpBar; // �� ü�¹�

    // ���¸� ��Ƶ� ������ �����, �⺻���·� ����
    public EnemyState eState = EnemyState.Idle;

    Transform player; // �÷��̾�
    float distance; // �÷��̾���� �Ÿ�

    NavMeshAgent agent; // NavMeshAgent ������Ʈ



    void Damaged(float damage)
    {
        hp -= damage; // ���ݹ��� ��������ŭ ü�� ����

        // ������ ü���� ü�¹ٿ� ǥ��
        hpBar.value = hp;

        agent.isStopped = true;// �̵��ߴ�
        agent.ResetPath(); // ��� �ʱ�ȭ

        if (hp > 0) // ü���� �����ִٸ�
        {
            eState = EnemyState.Damaged; // �ǰ� ���·� ��ȯ
        }
        else // ü���� �������� �ʴٸ�
        {
            eState = EnemyState.Dead;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // Player ������Ʈ�� ã�� �÷��̾��� transtorm ������Ʈ ��������
        player = FindObjectOfType<Player>().transform;

        agent = GetComponent<NavMeshAgent>(); // Nav mesh Agent ������Ʈ ������
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �÷��̾� ���� �Ÿ� ���
        distance = Vector3.Distance(transform.position, player.position);

        // �⺻, �̵�, ���� ������ �� �� �� ������
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



    private void Idle() // �⺻ ����
    {
        if (distance <= 8) // �÷��̾���� �Ÿ��� 8 �̻��̸�
        {
            eState = EnemyState.Walk; // �̵� ���·� ��ȯ

            agent.isStopped = false; // �̵� ����
        }
    }

    private void Walk()
    {
        if (distance > 8) // �÷��̾���� �Ÿ��� 8 ���� ũ�ٸ�
        {
            eState = EnemyState.Idle; // �⺻ ���·� ��ȯ
            agent.isStopped = true; // �̵� �ߴ�
            agent.ResetPath(); // ��� �ʱ�ȭ
        }

        else if (distance <= 2) // �÷��̾���� �Ÿ��� 2 ���϶��
        {
            eState = EnemyState.Attack; // �������� ��ȯ
            agent.isStopped = true; // �̵� �ߴ�
            agent.ResetPath(); // ��� �ʱ�ȭ
        }

        // �� �� ��Ȳ : �ٸ� ���·� ��ȯ���� ���� ��
        else
        {
            // �÷��̾� ��ġ�� �������� ����
            agent.SetDestination(player.position);
        }
    }

    private void Attack()
    {
        // �÷��̾���� �Ÿ��� 2���� ũ�ٸ�
        if (distance > 2)
        {
            eState = EnemyState.Walk; // �̵� ���·� ��ȯ
            agent.isStopped = false; // �̵� ����
        }
    }
}
