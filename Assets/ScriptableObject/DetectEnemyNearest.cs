using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class DetectEnemyNearest : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.enemyObj != null) return State.Success;

        Collider[] rangeChecks = Physics.OverlapSphere(context.transform.position, 50, LayerMask.GetMask("Warrior", "Archer"));
        if(rangeChecks == null)
        {
            rangeChecks = Physics.OverlapSphere(context.transform.position, 50, LayerMask.GetMask("Warrior", "Archer"));
        }
        if(rangeChecks != null)
        {
            float distance = 100000f;
            GameObject targetObj = null;
            foreach (Collider obj in rangeChecks)
            {
                if (obj.gameObject.tag == context.gameObject.tag)
                    continue;

                Transform target = obj.transform;
                Vector3 directionToTarget = target.position - context.transform.position;

                if (directionToTarget.magnitude < distance)
                {
                    targetObj = obj.gameObject;

                    distance = directionToTarget.magnitude;

                }

            }
            blackboard.enemyObj = targetObj;
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
        
    }
}
