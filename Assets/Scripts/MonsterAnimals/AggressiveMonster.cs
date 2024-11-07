using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggressiveMonster : Monster
{
    public AggressiveMonsterData monsterData;

    [Header("AI")] 
    private NavMeshAgent agent;
    private EAIState aiState;
    
    private float playerDistance;
    private float lastAttackTime;
    
    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    public Transform targetPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponents<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(EAIState.Wandering);
        agent.SetDestination(targetPosition.position);
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
        
        animator.SetBool("isMoving", aiState != EAIState.Idle);

        switch (aiState)
        {
            case EAIState.Idle:
            case EAIState.Wandering:
                PassiveUpdate();
                break;
            case EAIState.Attacking:
                AttackingUpdate();
                break;
        }
    }

    public override void SetState(EAIState state)            //각 각 만들것 
    {
        aiState = state;

        switch (state)
        {
            case EAIState.Idle:
                agent.speed = monsterData.walkSpeed;
                agent.isStopped = true;
                break;
            case EAIState.Wandering:
                agent.speed = monsterData.walkSpeed;
                agent.isStopped = false;
                break;
            case EAIState.Attacking:
                agent.speed = monsterData.runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / monsterData.walkSpeed;
    }
    
    public override void PassiveUpdate()
   {
       if (playerDistance < monsterData.detectDistance)
       {
           SetState(EAIState.Attacking);
       }
       else
       {
           SetState(EAIState.Wandering);
           agent.SetDestination(targetPosition.position);
       }
   }

    public override void AttackingUpdate()
    {
        if (playerDistance < monsterData.attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > monsterData.attackRate)
            {
                lastAttackTime = Time.time;
                //todo : 플레이어 데미지 입히는 코드 넣기
                //PlayerManager.Instance.Player.playerController.GetComponent<IDamagalbe>().TakePhysicalDamage(damage);
                PlayerManager.Instance.Player.playerCondition.uiCondition.health.Subtract(monsterData.damage);
                animator.speed = 1;
                animator.SetTrigger("isAttack");
            }
        }
        else
        {
            if (playerDistance < monsterData.detectDistance)        //추적하고 있는 범위안에 있을떄
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();       //path 활용가능성 무궁무진 참고
                if (agent.CalculatePath(PlayerManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(PlayerManager.Instance.Player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(EAIState.Wandering);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(EAIState.Wandering);
            }
        }
    }

    public override bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = PlayerManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < monsterData.fieldOfView * 0.5f;
    }
    
    public override void TakeDamage(int amount, Transform attacker)
    {
        monsterData.health -= amount;
        if (monsterData.health <= 0)
        {
            Die();
        }
    }

    public override void Heal(int amount)
    {
        monsterData.health += amount;
    }

    private void Die()
    {
        for (int i = 0; i < monsterData.dropOnDeath.Length; i++)
        { 
            //아이템 저장하는 변수 이름 가져와서 dropPrefab에 넣기
            Instantiate(monsterData.dropOnDeath[i], transform.position + Vector3.forward, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}
