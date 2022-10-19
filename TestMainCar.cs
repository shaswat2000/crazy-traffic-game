using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class TestMainCar : MonoBehaviour
{
    public WheelCollider FL;
    public WheelCollider FR;
    public WheelCollider RL;
    public WheelCollider RR;
    public GameObject WFL;
    public GameObject WFR;
    public GameObject WRL;
    public GameObject WRR;
    //public GameObject LRButton;

    public float dot;
    public float velocity;
    public float thrust;
    public float turn;
    public float brake;
    public int GearValue;
    private float[] GearRatio;
    private float[] GearSpeed = {13.0f, 9.0f, 12.0f, 13.0f, 14.0f, 15.0f};
    public float gearFactor;
    public float MaxTurn;
    public float MaxBrake;
    private Vector3 pos;
    private Quaternion quat;
    public float enginePitch;
    public static int carCount0 = 0;
    public int carCount;
    public float engine_Pitch;
    private int d;
    private char c;
    public int forward;
    public int reverse;

    void Start()
    {
        GearRatio = new float[] { 0.4f, 0.4f, 0.6f-gearFactor, 0.8f-gearFactor, 1.0f-gearFactor, 1.2f-gearFactor };
        GearValue = 1;
        //this.gameObject.GetComponent<AudioSource>().pitch = engine_Pitch;
        enginePitch = engine_Pitch;
        carCount = carCount0;
        d = 0;
        c = '\0';
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, -5.0f);
        //Screen.SetResolution(640, 480, true);
    }

    void FixedUpdate()
    {

        velocity = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        enginePitch = engine_Pitch + (0.1f * ((velocity-GearSpeed[GearValue])/2.0f));
        if(GearValue!=5 && velocity>(GearSpeed[GearValue+1] + 3.0f))
        {
            GearValue++;
        }
        else if(GearValue!=1 && velocity<GearSpeed[GearValue]-3.0f)
        {
            GearValue--;
        }

        //thrust = Input.GetAxis("Vertical");
        //turn = Input.GetAxis("Horizontal");
        //brake = Input.GetAxis("Jump");

        FL.GetWorldPose(out pos, out quat);
        WFL.transform.position = pos;
        WFL.transform.rotation = Quaternion.Euler(quat.eulerAngles);
        FR.GetWorldPose(out pos, out quat);
        WFR.transform.position = pos;
        WFR.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, quat.eulerAngles.y, quat.eulerAngles.z);
        RL.GetWorldPose(out pos, out quat);
        WRL.transform.position = pos;
        WRL.transform.rotation = Quaternion.Euler(quat.eulerAngles);
        RR.GetWorldPose(out pos, out quat);
        WRR.transform.position = pos;
        WRR.transform.rotation = Quaternion.Euler(quat.eulerAngles.x, quat.eulerAngles.y, quat.eulerAngles.z);

        /*if (brake > 0.5f)
        {
            this.gameObject.GetComponent<AudioSource>().pitch = engine_Pitch-0.06f;
        }
        else if(brake<=0.5)
        {
            this.gameObject.GetComponent<AudioSource>().pitch = enginePitch;
        }*/
        //this.gameObject.GetComponent<AudioSource>().pitch = enginePitch;

        FL.steerAngle = MaxTurn * turn;
        FR.steerAngle = MaxTurn * turn;

        RL.brakeTorque = MaxBrake * brake;
        RR.brakeTorque = MaxBrake * brake;
        FL.brakeTorque = MaxBrake * brake;
        FR.brakeTorque = MaxBrake * brake;

        if (thrust>=0) 
        {

            RL.motorTorque = 100000.0f * GearRatio[GearValue] * thrust * RL.radius;
            RR.motorTorque = 100000.0f * GearRatio[GearValue] * thrust * RL.radius;

        }
        else if(thrust<0)
        {
            RL.motorTorque = 10000.0f * GearRatio[0] * thrust * RL.radius;
            RR.motorTorque = 10000.0f * GearRatio[0] * thrust * RL.radius;

        }
    }
    void Update()
    {

        turn = Input.acceleration.x;
        /*float rot = this.gameObject.transform.rotation.eulerAngles.y;
        dot = Vector3.Dot((new Vector3(this.gameObject.GetComponent<Rigidbody>().velocity.x, 0.0f, this.gameObject.GetComponent<Rigidbody>().velocity.z)), (new Vector3((float)(Math.Sin(rot)), 0.0f, (float)Math.Cos(rot))));
        if (dot > 0.2f)
        {
            thrust = forward;
            brake = reverse;
        }
        else if (dot < -0.2f)
        {
            thrust = reverse;
            brake = forward;
        }
        else
        {
            thrust = forward + reverse;
            brake = 0.0f;
        }*/
        
        if (brake > 0.0f)
        {
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }
}