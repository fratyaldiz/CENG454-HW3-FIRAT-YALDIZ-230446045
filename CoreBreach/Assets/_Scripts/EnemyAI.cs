using UnityEngine;

// robot enemy.
// movement and attack come from strategy components.
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float attackCooldown =1.5f;

    private Transform coreTarget;
    private IMovementStrategy myMovement;
    private IAttackStrategy myAttack;
    private float nextAttackTime;

    private void Start()
    {
        // get core from static reference
        if (Core.Instance!= null)
        {
            coreTarget =Core.Instance.transform;
        }
        else
        {
            Debug.LogWarning("No Core in scene,enemy disable himself");
            enabled =false;
            return;
        }

        // get strategies from components.
        // GetComponent finds anything that implement the interface.
        myMovement= GetComponent<IMovementStrategy>();
        myAttack =GetComponent<IAttackStrategy>();

        if (myMovement== null)
        {
            Debug.LogWarning("Enemy has no IMovementStrategy");
        }
        if (myAttack == null)
        {
            Debug.LogWarning("Enemy has no IAttackStrategy");
        }
    }

    private void Update()
    {
        if (coreTarget ==null) return;

        float distance =Vector3.Distance(transform.position, coreTarget.position);

        // movement strategy decide if we should stop
        bool shouldStop = false;
        if (myMovement !=null)
        {
            shouldStop= myMovement.ShouldStopAtRange(distance);
        }

        // if not stopping, move with strategy
        if (!shouldStop && myMovement != null)
        {
            myMovement.Move(transform, coreTarget);
        }

        // attack if in range
        if (distance <attackRange)
        {
            transform.LookAt(coreTarget);

            if (Time.time >=nextAttackTime && myAttack != null)
            {
                myAttack.Attack(coreTarget);
                nextAttackTime = Time.time +attackCooldown;
            }
        }
    }
}