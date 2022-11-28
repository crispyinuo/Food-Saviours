using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    private Animator anim;
	private CharacterController controller;
	public float m_attackRange = 1f;
    public float m_searchRange = 5f;
    // private int foodCount = 0;
	public float m_speed = 3.0f;
	public float turnSpeed = 150.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;
    private State m_state = State.Idle;
    private Player attackTarget = null;
    private NavMeshAgent agent;
    private bool attack = false;

    public enum State {
        Idle,
        Walking,
        Attacking
    }

    public void MoveToLocation(Vector3 targetPoint)
    {
        agent.destination = targetPoint;
        agent.isStopped = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent> ();    
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.speed = m_speed;
    }

    // Update is called once per frame
    void Update()
    {
        //REeset the MoveVector
        Vector3 moveVector = Vector3.zero;
 
         //Check if cjharacter is grounded
         if (controller.isGrounded == false)
         {
             //Add our gravity Vecotr
             moveVector += Physics.gravity;
         }

          switch (m_state) {
            case State.Idle:
                this.attack=false;
                if(attackTarget==null){
                    GetTarget();
                    if(attackTarget == null){
                        moveVector = Vector3.zero;
                    }
                }
                // set Direction of the Player
                if(attackTarget!=null){
                    m_state = State.Walking;
                    anim.SetTrigger("Walk");
                    agent.SetDestination(attackTarget.transform.position);
                }
                break;
            case State.Walking:
                this.attack=false;
                GetTarget();
                if(attackTarget==null){
                    moveVector = Vector3.zero;
                    // if attackTarget disappear, backToIdle
                    m_state = State.Idle;    
                    anim.SetTrigger("Idle"); 
                }

                // set Direction of the Player
                if(attackTarget!=null){
                    agent.SetDestination(attackTarget.transform.position);
                }

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_attackRange);
                for (int i = 0; i < hitColliders.Length; i++){
                    GameObject hitCollider = hitColliders[i].gameObject;
                    if (hitCollider.GetComponent<Player>()!=null)
                    {
                        attackTarget = hitCollider.GetComponent<Player>();
                        Debug.Log("Found the Player!");
                        m_state = State.Attacking;
                        break;
                    }
                }
                break;
            case State.Attacking:
                if(attackTarget!=null){
                    attack = true;
                    anim.SetTrigger("Attack");
                }
                if(attackTarget==null){
                    attack = false;
                    m_state = State.Walking;
                    anim.SetTrigger("ReloadWalk");
                }
                break;  
        }

        //Apply our move Vector
         controller.Move(moveVector * Time.deltaTime);
 
    }

    void GetTarget(){
        var player = FindObjectOfType<Player>();
   
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        
        // if player is within the searchRange, set the attackTarget
        if(dist<=m_searchRange){
            this.attackTarget = player;
        }
    }
}
