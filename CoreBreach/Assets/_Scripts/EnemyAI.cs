using UnityEngine;
using UnityEngine.AI; // we need this for robot walk

public class EnemyAI : MonoBehaviour
{
    private Transform coreTarget;
    private IAttackStrategy myAttack;
    private NavMeshAgent agent;

    private void Start()
    {
        // robots should find core in map to kill it
        GameObject coreObject = GameObject.Find("Core");
        if (coreObject!= null)
        {
            coreTarget= coreObject.transform;
        }

        agent= GetComponent<NavMeshAgent>();
        
        myAttack =GetComponent<IAttackStrategy>(); 
    }

    private void Update()
    {
        if (coreTarget !=null && agent !=null)
        {
            // go to core position
            agent.SetDestination(coreTarget.position);

            // look how far is core
            float distance = Vector3.Distance(transform.position, coreTarget.position);
            
            // if robot close enough, stop and shoot
            if (distance < 10f) 
            {
                agent.isStopped = true; // stop walk
                transform.LookAt(coreTarget); // look at core

                if (myAttack != null)
                {
                    myAttack.Attack(coreTarget); // use strategy pattern!
                }
            }
            else
            {
                agent.isStopped = false; // continue walk
            }
        }
    }
}