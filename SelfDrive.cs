using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Specialized;

public class SelfDrive : MonoBehaviour
{
    public WheelCollider FL;
    public WheelCollider FR;
    public WheelCollider RL;
    public WheelCollider RR;
    public GameObject WFL;
    public GameObject WFR;
    public GameObject WRL;
    public GameObject WRR;

    public float thrust;
    public float turn;
    public float brake;
    public float velocity;
    public float IdealSpeed;
    public float IdealSpeed2;
    private float[] GearRatio;
    private int GearValue;
    public float gearFactor;
    private float[] GearSpeed = { 0.0f, 17.0f, 33.0f, 50.0f, 70.0f, 90.0f };
    public float PivotOffsetmag;
    public float PivotOffsetdir;
    public float MaxTurn;
    public float MaxBrake;
    private Vector3 pos;
    private Quaternion quat;
    public float StopTime = 0.0f;
    public int stop = 0;
    public int stop2 = 0;
    public float deviation = 0.0f;
    public float deviation2 = 0.0f;
    public float t = 0.0f;
    public int i;
    public int veto = 0;
    public int turnValue;
    public int orientation = 0;
    public float displacement;
    public float diff1y;
    public float diff2y;
    public String signal;
    public float enginePitch;
    public int safe = 0;
    //public Vector3 vel2;
    public float vel2;
    public int brakelights;
    public int vision;
    private GameObject refer;
    public float engine_Pitch;

    void Start()
    {
        GearRatio = new float[] { 0.4f, 0.4f, 0.6f - gearFactor, 0.8f - gearFactor, 1.0f - gearFactor, 1.2f - gearFactor };
        GearValue = 1;
        //IdealSpeed = UnityEngine.Random.Range(9.0f, 10.0f);
        IdealSpeed = UnityEngine.Random.Range(5.0f, 7.0f);
        IdealSpeed2 = IdealSpeed;
        turn = 0.0f;
        vision = 4;
        //this.gameObject.GetComponent<AudioSource>().pitch = engine_Pitch;
        enginePitch = 0.7f;
        refer = GameObject.FindGameObjectsWithTag("Player")[0];
        refer.GetComponent<TestMainCar>().carCount -= 1;
        if (stop != 11)
        {
            safe = 1;
        }
    }

    void FixedUpdate()
    {
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

        velocity = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

        if (stop == 1) //stop at signal
        {
            stop2 = stop;
            veto = 2;
            brake = 1.0f;
            StopTime = StopTime - Time.deltaTime;



            if (StopTime < 0)
            {
                stop = 0;
                StopTime = 0.0f;
                GearValue = 0;
                veto = 0;
                quat = this.gameObject.transform.rotation;
                brake = 0.0f;
            }

        }

        else if (stop == 2) //stop before obstacle
        {
            brake = 1.0f;
        }

        if (stop == 9)
        {
            if(stop2==10)
            {
                IdealSpeed2 = IdealSpeed;
                stop2 = 9;
            }
            StopTime = 0.0f;
            if (brake < 0.8f)
            {
                turn = turn - ((deviation) / (90.0f)) - ((displacement) / 30.0f);
            }

            //brake = 0.0f;
        }
        if (stop == 10)
        {
            //IdealSpeed2 = IdealSpeed;
            stop2 = 10;
            if (turnValue != 2)
            {
                IdealSpeed2 = UnityEngine.Random.Range(4.0f, 5.0f);
            }
            StopTime = 0.0f;
            if (brake < 0.8f)
            {
                turn = turn - ((deviation) / (105.0f)) - ((displacement) / 30.0f);
            }

            //brake = 0.0f;
        }

        else if (stop == 11) //Parked car
        {
            veto = 4;
            turn = 0.0f;
            brake = 0.001f;

            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else if ((stop == 0) || (stop == 7)) //Default mode
        {
            veto = 0;
            thrust = 0.5f;
            if(stop2!=10)
            turn = 0.0f;
        }

        FL.steerAngle = MaxTurn * turn;
        FR.steerAngle = MaxTurn * turn;

        RL.brakeTorque = MaxBrake * brake;
        RR.brakeTorque = MaxBrake * brake;
        FL.brakeTorque = MaxBrake * brake;
        FR.brakeTorque = MaxBrake * brake;


        if(stop!=11)
        {
            //this.gameObject.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            //this.gameObject.GetComponent<AudioSource>().enabled = false;
        }

        if (thrust >= 0)
        {

            RL.motorTorque = 10000.0f * GearRatio[GearValue] * thrust * (IdealSpeed2 - velocity) * RL.radius;
            RR.motorTorque = 10000.0f * GearRatio[GearValue] * thrust * (IdealSpeed2 - velocity) * RL.radius;

        }
        /*else if (thrust < 0)
        {
            RL.motorTorque = 10000.0f * GearRatio[0] * thrust * RL.radius;
            RR.motorTorque = 10000.0f * GearRatio[0] * thrust * RL.radius;
        }*/
        vel2 = velocity;
    }

    void Update()
    {

        if ((brake > 0.0f || brakelights==1) && stop != 11)
        {
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            vel2 = -5.0f;
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            vel2 = 0.0f;
        }

    }
}
