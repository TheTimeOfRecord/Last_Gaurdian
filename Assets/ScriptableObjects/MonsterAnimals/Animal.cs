using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animal : MonoBehaviour
{
    [Header("Stats")] 
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath;

    [Header("AI")] 
    private UnityEngine.AI.NavMeshAgent agent;
    public float detectDistance;
    private EAIState aiState;

    [Header("Wandering")] 
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")] public float damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldOfView = 120f;
    
    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponents<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(EAIState.Wandering);
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

    public void SetState(EAIState state)
    {
        aiState = state;

        switch (state)
        {
            case EAIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case EAIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case EAIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }

    private void PassiveUpdate()
    {
        if (aiState == EAIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(EAIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
    }

    private void WanderToNewLocation()      //동물들한테만 적용
    {
        if (aiState != EAIState.Idle) return;
        
        SetState(EAIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    private Vector3 GetWanderLocation()
    {
        UnityEngine.AI.NavMeshHit hit;

        int i = 0;

        do
        {
            UnityEngine.AI.NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, UnityEngine.AI.NavMesh.AllAreas);
            i++;
            if(i == 30) break;
        } while (Vector3.Distance(transform.position, hit.position) < detectDistance);
        
        return hit.position;
    }

    private void AttackingUpdate()
    {
        
    }
}
