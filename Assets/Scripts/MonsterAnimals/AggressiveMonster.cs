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
    
    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    public Vector3 targetPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponents<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(EAIState.Wandering);
        agent.SetDestination(targetPosition);
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
       //todo : 동물한테만 적용 동물이 피해를 당하면 공격하는 로직 작성
       if (playerDistance < monsterData.detectDistance)
       {
           SetState(EAIState.Attacking);
       }
   }

    public override void AttackingUpdate()
    {
        if (playerDistance < monsterData.attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - monsterData.lastAttackTime > monsterData.attackRate)
            {
                monsterData.lastAttackTime = Time.time;
                //todo : 플레이어 데미지 입히는 코드 넣기
                //PlayerManager.Instance.Player.playerController.
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
}
