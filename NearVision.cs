using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NearVision : MonoBehaviour
{
    public int counter;
    public string tag;
    public int i;
    public float vel;
    void Start()
    {
        counter = 0;
        i = 0;
        vel = 0.0f;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //if (/*col.gameObject.GetComponent<Rigidbody>() != null &&*/ Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity) >= 0.0f && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop != 10)
        {
            tag = col.gameObject.transform.parent.gameObject.tag;
            if (tag.Contains("signal"))
            {
                i = 1;
            }
            if ((tag == "selfdrive" || tag == "Player" || i==1) && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision > 1)
            {
                counter++;
                this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision = 1;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        //if (/*col.gameObject.GetComponent<Rigidbody>() != null &&*/ Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity) >= 0.0f && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop != 10)
        {
            tag = col.gameObject.transform.parent.gameObject.tag;
            if ((tag == "selfdrive" || tag == "Player" || i==1) && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision == 1)
            {
                if (col.gameObject.GetComponent<Rigidbody>().velocity.magnitude < this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
                {
                    this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed2 = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude + 2.0f + 0.8f * (col.gameObject.GetComponent<Rigidbody>().velocity.magnitude - this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
                    this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().brakelights = 1;
                    vel = col.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        //if (/*col.gameObject.GetComponent<Rigidbody>() != null &&*/ Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity) >= 0.0f && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop != 10)
        {
            tag = col.gameObject.transform.parent.gameObject.tag;
            if ((tag == "selfdrive" || tag == "Player" || i == 1) && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision == 1)
            {
                counter--;
                if (counter <= 0)
                {
                    this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision = 4;
                    this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed2 = this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed;
                    this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().brakelights = 0;
                }
            }
            if (tag.Contains("signal"))
            {
                i = 0;
            }                         
        }
    }
}
