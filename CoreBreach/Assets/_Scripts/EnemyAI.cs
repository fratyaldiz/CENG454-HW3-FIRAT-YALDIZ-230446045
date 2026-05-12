using UnityEngine;

// enemy ai. it run to core nonstop.
// movement come from strategy. no attack, just rush to core.
public class EnemyAI : MonoBehaviour
{
    private Transform coreTarget;
    private IMovementStrategy myMovement;

    private void Start()
    {
        if (Core.Instance !=null)
        {
            coreTarget = Core.Instance.transform;
        }
        else
        {
            Debug.LogWarning("No Core in scene, enemy disable himself");
            enabled =false;
            return;
        }

        myMovement= GetComponent<IMovementStrategy>();
    }

    private void Update()
    {
        if (coreTarget ==null) return;

        // always look forward to core and move
        if (myMovement!= null)
        {
            myMovement.Move(transform, coreTarget);
        }
    }
}