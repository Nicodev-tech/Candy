using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float losePlayerTimer;
    private bool seenPlayer;
    public override void Enter()
    {
        seenPlayer = true;
    }
    public override void Exit()
    {
        seenPlayer = false;
    }
    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            enemy.Agent.SetDestination(enemy.Player.transform.position);
            enemy.LastKnowPos = enemy.Player.transform.position;
            //Debug.Log(enemy.Agent.remainingDistance);
            if (seenPlayer) 
            {
                SoundFXManager.Instance.PlayRandomSoundFXClip(enemy.vistaNeneSound, enemy.transform, 1f);
                seenPlayer = false;
            }
            if (enemy.Caramelo != null)
            {
                //cambiar estado a perseguir caramelo
            }
            if(enemy.Agent.remainingDistance < 1.2f) 
            {
                Debug.Log("DAME CARAMAAELELELO");
                PlayerMovement n = enemy.Player.GetComponent<PlayerMovement>();
                n.FinalMalo();
            }
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
