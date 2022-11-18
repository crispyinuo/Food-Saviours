using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject m_target;
    public Vector3 m_targetOffset = new Vector3(0.0f, 1.0f, 0.0f);
    float m_distanceCurrent;
    float set_distance;
    float m_azimuth;
    float m_elevation;
    bool collision = false;
    float camera_smoothSpeed = 10.0f;
    public float m_panSpeed = 180.0f;   // degrees per second
    public float m_tiltSpeed = 180.0f;  // degrees per second
    public float m_tiltMax = 0.0f;      // degrees
    public float m_tiltMin = -60.0f;    // degrees
    private CamInput cameraInput = new CamInput();
   // public Joystick joystick;


    // Start is called before the first frame update
    void Start()
    {
        //Camera.main.transform.position = m_target.transform.TransformPoint(m_targetOffset);
        Vector3 targetPos = m_target.transform.position;
        Vector3 camPos = transform.position;
        Vector3 delta = camPos - targetPos;
        m_distanceCurrent = delta.magnitude;
        m_azimuth = Mathf.Atan2(delta.x,delta.z);
        m_elevation = Mathf.Atan2(delta.y,Mathf.Sqrt(delta.x*delta.x+delta.z*delta.z));
        set_distance = m_distanceCurrent;
    }

    // Update is called once per frame
    void Update()
    {
        cameraInput.azimuth = 0.0f;
        cameraInput.elevation = 0.0f;

        // float vert = joystick.Vertical;
        // float horiz = joystick.Horizontal;
        float vert = 0;
        float horiz = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
            horiz -= 1.0f;
        if (Input.GetKey(KeyCode.RightArrow))
            horiz += 1.0f;
        if (Input.GetKey(KeyCode.UpArrow))
            vert += 1.0f;
        if (Input.GetKey(KeyCode.DownArrow))
            vert -= 1.0f;

        cameraInput.azimuth = horiz;
        cameraInput.elevation = vert;

         //check for collision
        RaycastHit hit;

        // Vector3 Offset_position = m_target.transform.position+m_targetOffset;
        // if (Physics.SphereCast(Offset_position, 0.0f, transform.position-Offset_position, out hit, set_distance))
        // {
        //     collision = true;
        // }else{
        //     collision = false;
        // }

        // if(collision==true){
        //     m_distanceCurrent = hit.distance;
        //     Debug.Log("blocked by "+hit.collider.gameObject.name);
        // }else{
        //     if(m_distanceCurrent<=set_distance - camera_smoothSpeed*Time.deltaTime){
        //         m_distanceCurrent+=camera_smoothSpeed*Time.deltaTime;   
        //     }else if(m_distanceCurrent<set_distance){
        //         m_distanceCurrent = set_distance;
        //     }
        //     //m_distanceCurrent = set_distance;
        //     Debug.Log("back to normal");
        // }
    }

    void LateUpdate(){
        Vector3 newPos;
        m_azimuth += cameraInput.azimuth*m_panSpeed*Time.deltaTime;
        m_elevation += cameraInput.elevation*m_tiltSpeed*Time.deltaTime;

        m_elevation = Mathf.Clamp(m_elevation, -m_tiltMax, -m_tiltMin);

        if(m_azimuth<-180.0f){
            m_azimuth+=360.0f;
        }
        if(m_azimuth>180.0f){
            m_azimuth-=360.0f;
        }

        Vector3 targetPos = m_target.transform.position + m_targetOffset;
        newPos.y = m_distanceCurrent*Mathf.Sin(Mathf.Deg2Rad*m_elevation);
        newPos.x = m_distanceCurrent*Mathf.Cos(Mathf.Deg2Rad*m_elevation)*Mathf.Cos(Mathf.Deg2Rad*m_azimuth);
        newPos.z = m_distanceCurrent*Mathf.Cos(Mathf.Deg2Rad*m_elevation)*Mathf.Sin(Mathf.Deg2Rad*m_azimuth);
        transform.position = newPos + targetPos;

        transform.LookAt(targetPos);
    }

    class CamInput{
        public float azimuth = 0.0f;
        public float elevation = 0.0f;
    }
}
