using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float losePlayerTimer;
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            enemy.Agent.SetDestination(enemy.Player.transform.position);
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else //pierde al jugador
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > enemy.losePlayerTime)
            {
                //cambiar a estado de busqueda
                stateMachine.ChangeState( new SearchState());
            }
        }
    }
}
