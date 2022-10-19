using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SelfNavigation : MonoBehaviour
{
    public WheelCollider FL;
    public WheelCollider FR;
    public WheelCollider RL;
    public WheelCollider RR;
    public Rigidbody WFL;
    public Rigidbody WFR;
    public Rigidbody WRL;
    public Rigidbody WRR;
    public Text t;

    private float a;
    private float thrust;
    private float turn;
    private float brake;
    private float CurrentSpeed;
    private float IdealSpeed;
    private float Enginerpm;
    private float EngineRadius = 0.4f;
    private float[] GearRadius = { 5.0f, 3.0f, 1.0f, 0.4f, 0.2f };
    private int GearValue;
    private float k = 100;
    public float MaxTurn;
    public float MaxBrake;
    private int stop = 0;
    private float StopTime;

    void Start()
    {
        Enginerpm = 0.0f;
        GearValue = 0;
    }

    void FixedUpdate()
    {
        

        //thrust = Input.GetAxis("Vertical");
        //turn = Input.GetAxis("Horizontal");
        //brake = Input.GetAxis("Jump");

        if(stop==1)
        {
            brake = 1.0f;
            RL.brakeTorque = MaxBrake * brake;
            RR.brakeTorque = MaxBrake * brake;
            FL.brakeTorque = MaxBrake * brake;
            FR.brakeTorque = MaxBrake * brake;
            StopTime = StopTime + Time.deltaTime;
            if (StopTime == 5)
            {
                stop = 0;
                StopTime = 0;
            }
        }
        else if (stop==0)
        {

            thrust = 0.7f;
            turn = 0.0f;
            brake = 0.0f;

            Enginerpm = Enginerpm + (thrust * 100) - (0.05f * Enginerpm);
            IdealSpeed = 2 * (22 / 7) * RL.radius * ((Enginerpm * EngineRadius) / GearRadius[GearValue]) * 60 * 0.001f;
            CurrentSpeed = 2 * (22 / 7) * RL.radius * ((RL.rpm + RR.rpm) / 2) * 60 * 0.001f;

            RL.motorTorque = thrust * k * (IdealSpeed - CurrentSpeed);
            RR.motorTorque = thrust * k * (IdealSpeed - CurrentSpeed);

            t.text = "Speed: " + CurrentSpeed + " " + Enginerpm + " " + GearValue;

            FL.steerAngle = MaxTurn * turn;
            FR.steerAngle = MaxTurn * turn;

            RL.brakeTorque = MaxBrake * brake;
            RR.brakeTorque = MaxBrake * brake;
            FL.brakeTorque = MaxBrake * brake;
            FR.brakeTorque = MaxBrake * brake;

            WFL.GetComponent<Rigidbody>().angularVelocity = new Vector3(-FL.rpm * 0.0167f * 6.28319f, 0.0f, 0.0f);
            WFR.GetComponent<Rigidbody>().angularVelocity = new Vector3(-FR.rpm * 0.0167f * 6.28319f, 0.0f, 0.0f);
            WRL.GetComponent<Rigidbody>().angularVelocity = new Vector3(-RL.rpm * 0.0167f * 6.28319f, 0.0f, 0.0f);
            WRR.GetComponent<Rigidbody>().angularVelocity = new Vector3(-RR.rpm * 0.0167f * 6.28319f, 0.0f, 0.0f);



            if ((Enginerpm < 400) && (GearValue != 0))
            {
                GearValue = GearValue - 1;
                Enginerpm = ((CurrentSpeed * GearRadius[GearValue]) / EngineRadius) * 1000 * 0.002653f / RL.radius;
            }
            else if ((Enginerpm > 1200) && (GearValue != 4))
            {
                GearValue = GearValue + 1;
                Enginerpm = ((CurrentSpeed * GearRadius[GearValue]) / EngineRadius) * 1000 * 0.002653f / RL.radius;
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "guide")
        {
           
            this.gameObject.transform.Translate(2.0f, 0.0f, 0.0f);

        }

    }
}
