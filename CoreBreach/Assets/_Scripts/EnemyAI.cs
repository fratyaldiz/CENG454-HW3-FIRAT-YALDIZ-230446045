using UnityEngine;
using UnityEngine.AI;

// robot enemy. it want to go to core and attack it.
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackRange =10f;
    [SerializeField] private float attackCooldown= 1.5f;

    private Transform coreTarget;
    private IAttackStrategy myAttack;
    private NavMeshAgent agent;
    private float nextAttackTime;

    private void Start()
    {
        // get core from static reference. fast and clean.
        if (Core.Instance !=null)
        {
            coreTarget= Core.Instance.transform;
        }
        else
        {
            // if no core in scene, this enemy is useless
            Debug.LogWarning("No Core in scene, enemy disable himself");
            enabled =false;
            return;
        }

        agent= GetComponent<NavMeshAgent>();
        myAttack = GetComponent<IAttackStrategy>();

        if (myAttack == null)
        {
            Debug.LogWarning("Enemy has no IAttackStrategy, can not attack");
        }
    }

    private void Update()
    {
        if (coreTarget== null || agent ==null) return;

        // walk to core
        agent.SetDestination(coreTarget.position);

        // check distance to decide attack or keep walking
        float distance =Vector3.Distance(transform.position, coreTarget.position);

        if (distance< attackRange)
        {
            // close enough, stop and attack
            agent.isStopped =true;
            transform.LookAt(coreTarget);

            if (Time.time >=nextAttackTime && myAttack != null)
            {
                myAttack.Attack(coreTarget);
                nextAttackTime= Time.time + attackCooldown;
            }
        }
        else
        {
            // too far, keep walking
            agent.isStopped =false;
        }
    }
}