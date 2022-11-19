using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;
		public float m_foodRange = 0f;
		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;


		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		}

		void Update (){
			if (Input.GetKey ("w")||Input.GetKey ("s")) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}
			if(Input.GetKey("space")){
				GetFood();
			}
			if(controller.isGrounded){
				moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			}

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}

		private void GetFood(){
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_foodRange);
            for (int i = 0; i < hitColliders.Length; i++){
                GameObject hitCollider = hitColliders[i].gameObject;
                if (hitCollider.GetComponent<Food>()!=null)
                {
					anim.SetTrigger("GetFood");
					hitCollider.GetComponent<Food>().Collect();
					Debug.Log("Collect the Food!");	
					if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime>0.5f) 
					{				
					}
                }
            }
		}
}
