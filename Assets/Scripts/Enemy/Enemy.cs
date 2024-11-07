using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private GameObject caramelo;
    private Vector3 lastKnowPos;
    [SerializeField] public AudioClip[] vistaNeneSound;
    [SerializeField] public AudioClip[] derrotadoNeneSound;



    public NavMeshAgent Agent { get => agent; }

    public GameObject Player { get => player; }

    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }

    public GameObject Caramelo { get => caramelo; set => caramelo = value; }
    //para debugeo
    [Header("Valores de Camino")]
    public Path path;
    public bool reversePath;

    public GameObject debugsphere;
    [Header("Valores de vista")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;
    public float losePlayerTime = 8f;
    public float timeBetweenPoints = 2f;
    [SerializeField]
    private string currentState;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        //debugsphere.transform.position = LastKnowPos;
    }
    public bool CanSeePlayer()
    {
        if (player != null) 
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position -(Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView) 
                { 
                    Ray ray = new Ray(transform.position + Vector3.up * eyeHeight,targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    
                    if(Physics.Raycast(ray, out hitInfo, sightDistance)) 
                    {
                        if(hitInfo.transform.gameObject == player)
                        {
                            //Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                        
                    }
                    
                }
            }
        
        }
        return false;
   }
    public bool CanSeeCandy()
    {

        if (Vector3.Distance(transform.position, caramelo.transform.position) < sightDistance)
            {
                Vector3 targetDirection = caramelo.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToCaramelo = Vector3.Angle(targetDirection, transform.forward);
                if (angleToCaramelo >= -fieldOfView && angleToCaramelo <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + Vector3.up * eyeHeight, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();

                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == caramelo)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }

                    }

                }
            }
        return false;
    }
    public int WalkPath(int WayPointIndex) 
    {
        if (reversePath)
        {
            WayPointIndex--;
        }
        else
        {
            WayPointIndex++;
        }
        if(WayPointIndex >= path.wayPoints.Count) 
        {
            WayPointIndex = 0;
        }
        if (WayPointIndex < 0)
        {
            WayPointIndex = path.wayPoints.Count - 1;
        }
        return WayPointIndex;
    }
}
