using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Soldier : MonoBehaviour
{
    private Animator anim;
	private CharacterController controller;
	public float m_attackRange = 1.5f;
    public float m_searchRange = 5f;
	public float m_speed = 2.0f;
	public float m_turnSpeed = 150.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;
    public float m_attackDelay = 5.0f;
    private State m_state = State.Idle;
    private Player attackTarget = null;
    private NavMeshAgent agent;
    private bool attack = false;
    private float timer = 0.0f;
	public AudioSource FireSound;
    public AudioSource ReloadSound;
    public enum State {
        Idle,
        Walking,
        Attacking
    }

    public void MoveToLocation(Vector3 targetPoint)
    {
     //   agent.destination = targetPoint;
        agent.isStopped = false;
        agent.SetDestination(targetPoint);
    }

    // Start is called before the first frame update
    void Start()
    {
		anim = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent> ();    
        agent.speed = m_speed;
      //  FireSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check State
        switch (m_state) {
            case State.Idle:
                this.attack=false;
                if(!GetTarget()){
                    agent.isStopped = true;
                }
                // set Direction of the Player
                if(GetTarget() && attackTarget!=null){
                    anim.SetTrigger("Walk");
                    m_state = State.Walking;
                    MoveToLocation(attackTarget.transform.position);
                  //  agent.SetDestination(attackTarget.transform.position);
                }
                break;
            case State.Walking:
                this.attack=false;
                if(!GetTarget()){
                    agent.isStopped = true;
                    // if attackTarget disappear, backToIdle
                    Debug.Log("I am Idle now");
                    anim.SetTrigger("Idle"); 
                    m_state = State.Idle;    
                }

                // set Direction of the Player
                if(attackTarget!=null){
                    MoveToLocation(attackTarget.transform.position);
                  //  agent.SetDestination(attackTarget.transform.position);
                }
                if(checkAttackTarget()){
                    m_state = State.Attacking;
                }
                break;
            case State.Attacking:
                //GetTarget();
                if(checkAttackTarget()){
                    attack = true;
                   // anim.SetTrigger("Attack");
                }else{
                    attack = false;
                    anim.SetTrigger("ReloadWalk");
                    m_state = State.Walking;
                }
                break;   
            } 
            timer+=Time.deltaTime;

            if(attack == true){
                if(timer>=m_attackDelay){
                    anim.SetTrigger("Attack");
                    timer = 0.0f;
                    attack = false;
                }
            }
 
    }

    bool GetTarget(){
        var player = FindObjectOfType<Player>();
         bool targetInSearch= false;
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        
        // if player is within the searchRange, set the attackTarget
        if(dist<=m_searchRange){
            this.attackTarget = player;
            targetInSearch= true;
        }
        return targetInSearch;
    }

    bool checkAttackTarget(){
        bool targetInRange = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_attackRange);
            for (int i = 0; i < hitColliders.Length; i++){
                GameObject hitCollider = hitColliders[i].gameObject;
                if (hitCollider.GetComponent<Player>()!=null)
                {
                    attackTarget = hitCollider.GetComponent<Player>();
                   // Debug.Log("Found the Player!");
                    targetInRange = true;
                    Vector3 angle =  attackTarget.transform.position - transform.position;
                    transform.forward = angle;
                  //  AttackThePlayer();
                    break;
                }
            }
        return targetInRange;
    }

    void AttackThePlayer(){
        if(attackTarget.lives <= 0){
            GameResult.didWin = false;
			SceneManager.LoadScene("End");
		}else{
            FireSound.Play();
            ReloadSound.Play();
            attackTarget.lives --;
        }
    }
}
