using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;
		public float m_foodRange = 0f;
		public GameObject maze;
		public float speed = 3.0f;
		public float turnSpeed = 150.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;
		private int foodCount = 0;
		public TextMeshProUGUI foodCounter;
		public TextMeshProUGUI lifeCounter;
    	public Joystick joystick;
		public float angleRange = 0.5f;
		public int lives = 3;
		private MazeRenderer myMaze;
		public AudioSource GetFoodSound;

		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
			myMaze = maze.GetComponent<MazeRenderer>();
		}

		void Update (){

			foodCounter.text = foodCount.ToString() + "/" + myMaze.getFoodCount().ToString();
			lifeCounter.text = lives.ToString();

        	if(foodCount == myMaze.getFoodCount()){
				//Destroy(attackTarget);
				SceneManager.LoadScene("End");
			}
			float vert = joystick.Vertical;
        	float horiz = joystick.Horizontal;
			// if (Input.GetKey ("w")||Input.GetKey ("s")) {
			// 	anim.SetInteger ("AnimationPar", 1);
			// }  else {
			// 	anim.SetInteger ("AnimationPar", 0);
			// }
			if(Input.GetKeyDown("space")){
				anim.SetTrigger("GetFood");
				GetFoodSound.Play();
			}

			vert+=Input.GetAxis("Vertical");
			if(vert!=0){
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}
			if(controller.isGrounded){
				moveDirection = transform.forward * vert * speed;
			}
			horiz += Input.GetAxis("Horizontal");
			transform.Rotate(0, horiz * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}
		
		public void onClickButton(){
			anim.SetTrigger("GetFood");
			GetFoodSound.Play();
		}
		
		public void GetFood(){
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_foodRange);
            for (int i = 0; i < hitColliders.Length; i++){
                GameObject hitCollider = hitColliders[i].gameObject;
                if (hitCollider.GetComponent<Food>()!=null)
                {
					Vector3 dir = hitColliders[i].GetComponent<Food>().transform.position-transform.position;
					Vector3 normal = dir.normalized;
					float angle = Mathf.Acos(Vector3.Dot(normal,transform.forward));
				//	Debug.Log("Angle" + angle);		
					if(angle<=angleRange){
						hitCollider.GetComponent<Food>().Collect();
						foodCount ++;
						Debug.Log("Collect the Food!");		
					}	
                }
            }
		}
}
