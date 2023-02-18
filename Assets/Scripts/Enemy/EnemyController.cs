using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _anim;

    [SerializeField] private Transform[] patrolPoints;
    private int _currentPatrolPoint;

    [SerializeField] private float distanceForChangePoint = 1f;

    [SerializeField] private float distanceToChasePlayer = 5f;
    [SerializeField] private float distanceBackToPatrolling = 10f;

    [SerializeField] private float patrollingSpeed = 3f, chaseSpeed = 4.5f;

    [SerializeField] private float timeBetweenAttack = 3f;
    private float _attackCounter;
    private bool _canAttack;
    
    private enum  EnemyState
    {
        Patrolling,
        Chasing
    }
    private EnemyState _enemyState;
    

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        foreach (var pp in patrolPoints)
        {
            pp.parent = null;
        }

        _enemyState = EnemyState.Patrolling;

        _canAttack = true;
    }

    private void Update()
    {
        ControllEnemy();

        if (_attackCounter>0)
        {
            _attackCounter -= Time.deltaTime;
        }
        else
        {
            _canAttack = true;
        }
    }
    
    private void ControllEnemy()
    {
        switch (_enemyState)
        {
            case EnemyState.Chasing:
                
                _anim.SetTrigger(TagManager.ENEMY_CHASING_TRIGGER);
                
                Chase();
                break;
            
            case EnemyState.Patrolling:
                _anim.SetTrigger(TagManager.ENEMY_PATROLL_TRIGGER);
                Patroll();
                break;
        }
    }

    private void Patroll()
    {
        _agent.SetDestination(patrolPoints[_currentPatrolPoint].position);
        _agent.speed = patrollingSpeed;

        if (Vector3.Distance(transform.position, patrolPoints[_currentPatrolPoint].position) <= distanceForChangePoint)
        {
            _currentPatrolPoint++;

            if (_currentPatrolPoint>=patrolPoints.Length)
            {
                _currentPatrolPoint = 0;
            }
        }

        if (Vector3.Distance(transform.position,PlayerHealth.Instance.transform.position)<=distanceToChasePlayer)
        {
            _enemyState = EnemyState.Chasing;
        }
    }

    private void Chase()
    {
        _agent.SetDestination(PlayerHealth.Instance.transform.position);
        _agent.speed = chaseSpeed;
        
        if (Vector3.Distance(transform.position,PlayerHealth.Instance.transform.position)>=distanceBackToPatrolling)
        {
            _enemyState = EnemyState.Patrolling;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG) && _canAttack)
        {
            _canAttack = false;
            _attackCounter = timeBetweenAttack;
            
           PlayerHealth.Instance.DamagePlayer();
        }
    }
}
