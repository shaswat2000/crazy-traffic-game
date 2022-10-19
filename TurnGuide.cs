using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class TurnGuide : MonoBehaviour
{
    private Vector3 diff1;
    private Vector3 diff2;
    private Vector3 pos1;
    private Vector3 pos2;
    private float a;
    private float b;
    private Vector3 disp;
    private float deviation;
    private float displacement;
    private int j = 0;
    public int turnValue = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerStay(Collider col)
    {
        if ((col.gameObject.tag == "Bumper") && (col.transform.parent.gameObject.GetComponent<SelfDrive>().veto == 1) && (this.gameObject.tag == col.transform.parent.gameObject.GetComponent<SelfDrive>().signal) && (turnValue == col.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue))
        {
            diff1 = ((col.transform.parent.gameObject.transform.eulerAngles));
            diff2 = (this.gameObject.transform.eulerAngles);

            if (((diff2.y < 360.0f) && (diff2.y > 270.0f)) && ((diff1.y >= 0.0f) && (diff1.y < 90.0f)))
            {
                diff1.y = diff1.y + 360.0f;
            }
            else if (((diff1.y < 360.0f) && (diff1.y > 270.0f)) && ((diff2.y >= 0.0f) && (diff2.y < 90.0f)))
            {
                diff2.y = diff2.y + 360.0f;
            }
            col.transform.parent.gameObject.GetComponent<SelfDrive>().diff1y = diff1.y;
            col.transform.parent.gameObject.GetComponent<SelfDrive>().diff2y = diff2.y;

            deviation = diff1.y - diff2.y;

            //if((deviation>-90.0f) && (deviation<90.0f))
            {
                col.transform.parent.gameObject.GetComponent<SelfDrive>().deviation = deviation * 2.53f;
                col.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 10;
                col.transform.parent.gameObject.GetComponent<SelfDrive>().turn = 0.0f;
                col.transform.parent.gameObject.GetComponent<SelfDrive>().i = 0;
                j = 1;

                diff2.y = 90.0f - diff2.y;
                diff2.y = Mathf.Deg2Rad * diff2.y;
                pos1 = col.transform.parent.gameObject.transform.position;
                pos2 = this.gameObject.transform.position;
                a = (float)((((pos2.x) * Math.Pow(Math.Tan(diff2.y), 2)) + ((pos1.z - pos2.z) * Math.Tan(diff2.y)) + pos1.x) / (1 + (Math.Pow(Math.Tan(diff2.y), 2))));
                b = (float)((((pos1.z) * Math.Pow(Math.Tan(diff2.y), 2)) + pos2.z + (pos1.x - pos2.x) * Math.Tan(diff2.y)) / (1 + Math.Pow(Math.Tan(diff2.y), 2)));
                disp = new Vector3((a - pos1.x), 0.0f, (b - pos1.z));
                if ((Vector3.SignedAngle(disp, col.transform.parent.gameObject.GetComponent<Rigidbody>().velocity, Vector3.up)) < 0)
                {
                    displacement = -1.0f * disp.magnitude;
                }
                else
                {
                    displacement = disp.magnitude;
                }
                col.transform.parent.gameObject.GetComponent<SelfDrive>().displacement = displacement;
                if (col.transform.parent.gameObject.GetComponent<SelfDrive>().veto < 1)
                {
                    col.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 1;
                }
                //col.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed = (col.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed)/10.5f;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Bumper") && (j == 1) && (col.transform.parent.gameObject.GetComponent<SelfDrive>().veto == 1))
        {
            col.transform.parent.gameObject.GetComponent<SelfDrive>().deviation = 0.0f;
            col.transform.parent.gameObject.GetComponent<SelfDrive>().diff1y = 0.0f;
            col.transform.parent.gameObject.GetComponent<SelfDrive>().diff2y = 0.0f;
            col.transform.parent.gameObject.GetComponent<SelfDrive>().displacement = 0.0f;
            col.transform.parent.gameObject.GetComponent<SelfDrive>().i = 1;
            //col.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 0;
            /*if (col.transform.parent.gameObject.GetComponent<SelfDrive>().veto == 1)
            {
                col.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 0;
                col.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 0;
            }*/
            //col.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed = (col.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed) * 10.5f;
            j = 0;
        }
    }
}
