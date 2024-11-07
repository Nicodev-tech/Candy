using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatrolState : BaseState
{
    public int wayPointIndex;
    public float waitTime;
    public override void Enter()
    {
        if (enemy.reversePath)
        {
            wayPointIndex = enemy.path.wayPoints.Count;
        }
        else {
            wayPointIndex = -1;
        }
    }
    public override void Exit()
    {
    }
    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer()) 
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
    public void PatrolCycle() 
    { 
        if (enemy.Agent.remainingDistance < 0.2f) 
        {
            waitTime += Time.deltaTime;
            if (waitTime > enemy.timeBetweenPoints)
            {
                wayPointIndex = enemy.WalkPath(wayPointIndex);
                


                enemy.Agent.SetDestination(enemy.path.wayPoints[wayPointIndex].position);
                waitTime = 0;
            }
        }

    }
}
