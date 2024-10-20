using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int wayPointIndex;
    public float waitTime;
    public override void Enter()
    {  
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
                if (wayPointIndex < enemy.path.wayPoints.Count - 1)
                    wayPointIndex++;
                else
                    wayPointIndex = 0;

                enemy.Agent.SetDestination(enemy.path.wayPoints[wayPointIndex].position);
                waitTime = 0;
            }
        }

    }
}
