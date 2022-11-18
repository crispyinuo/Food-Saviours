using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float m_walkSpeed = 4.0f;
    public float m_turnSpeed = 360.0f;  // degrees per second
    private CharacterController characterControl;
    private CharInput charInput = new CharInput();
    private Animator anim;
   // public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        characterControl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 forward = transform.TransformDirection(Vector3.forward);
       // characterControl.SimpleMove(m_walkSpeed*forward);
        charInput.directionMov = Vector3.zero;
        charInput.angle = Camera.main.transform.eulerAngles.y;

        // float vert = joystick.Vertical;
        // float horiz = joystick.Horizontal;

        float vert = 0;
        float horiz = 0;
         
        if (Input.GetKey(KeyCode.A)){
            horiz-=1.0f;       
        }
        if (Input.GetKey(KeyCode.D)){
            horiz+=1.0f;
        }
        if (Input.GetKey(KeyCode.W)){
            vert+=1.0f;
        }
        if (Input.GetKey(KeyCode.S)){
            vert-=1.0f;
        }
        if(Input.GetKeyDown(KeyCode.Space))
            setAttack();

        Vector3 camr = horiz*Camera.main.transform.right;
        camr.y = 0.0f;
        camr.Normalize();
        charInput.directionMov += camr;

        Vector3 camf = vert*Camera.main.transform.forward;
        camf.y = 0.0f;
        camf.Normalize();
        charInput.directionMov += camf;

        float cur = 0;
        while(cur<charInput.angle){
            float rotation = 0;
            if((charInput.angle - cur)>= m_turnSpeed*Time.deltaTime){
                rotation = m_turnSpeed*Time.deltaTime;
                cur += rotation;
            }
            else{
                rotation = charInput.angle - cur;
                cur += rotation;
            }
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, cur, transform.eulerAngles.z);
        }
        //transform.Rotate(charInput.angle, m_turnSpeed * Time.deltaTime);
        characterControl.SimpleMove(m_walkSpeed*charInput.directionMov);
        Vector3 charPos = transform.InverseTransformDirection(charInput.directionMov);
        anim.SetFloat("FwdBack", charPos.z);
        anim.SetFloat("RightLeft",charPos.x);


        if(charInput.attack == true){
            anim.SetTrigger("Attack");
            charInput.attack = false;
        }
    }

    public class CharInput
    {
        public Vector3 directionMov;
        public float angle;
        public bool attack = false;
    }

    public void setAttack(){
        charInput.attack = true;
    }
}
