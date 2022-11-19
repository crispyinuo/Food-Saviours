using UnityEngine;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;
		public float m_foodRange = 0f;
		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;
		private int foodCount = 0;
		public TextMeshProUGUI foodCounter;
    	public Joystick joystick;


		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		}

		void Update (){
			foodCounter.text = foodCount.ToString();

			float vert = joystick.Vertical;
        	float horiz = joystick.Horizontal;
			if (Input.GetKey ("w")||Input.GetKey ("s")) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}
			if(Input.GetKey("space")){
				GetFood();
			}

			vert+=Input.GetAxis("Vertical");
			if(controller.isGrounded){
				moveDirection = transform.forward * vert * speed;
			}
			horiz += Input.GetAxis("Horizontal");
			transform.Rotate(0, horiz * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}
		
		// //Detect collisions between the GameObjects with Colliders attached
		// void OnCollisionEnter(Collision collision)
		// {
		// 	//Check for a match with the specified name on any GameObject that collides with your GameObject
		// 	if (collision.gameObject.GetComponent<Food>()!=null)
		// 	{
		// 		anim.SetTrigger("GetFood");
		// 		collision.gameObject.GetComponent<Food>().Collect();
		// 		//If the GameObject's name matches the one you suggest, output this message in the console
		// 		Debug.Log("Do something here");
		// 	}
		// }

		public void GetFood(){
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_foodRange);
            for (int i = 0; i < hitColliders.Length; i++){
                GameObject hitCollider = hitColliders[i].gameObject;
                if (hitCollider.GetComponent<Food>()!=null)
                {
					anim.SetTrigger("GetFood");
					hitCollider.GetComponent<Food>().Collect();
					foodCount ++;
					Debug.Log("Collect the Food!");		
                }
            }
		}
}
