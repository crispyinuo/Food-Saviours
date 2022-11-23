using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private Animator anim;
	private CharacterController controller;
	public float m_foodRange = 0f;
	public float speed = 600.0f;
	public float turnSpeed = 400.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;
	private int foodCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
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
 
         //Apply our move Vector , remeber to multiply by Time.delta
         controller.Move(moveVector * Time.deltaTime);
 
    }
}
