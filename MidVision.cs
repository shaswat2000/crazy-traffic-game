using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidVision : MonoBehaviour
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

    // Update is called once per frame
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
                i = 0;
            }
            if ((tag == "selfdrive" || tag == "Player" || i==1) && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision > 2)
            {
                counter++;
                this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision = 2;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        //if (/*col.gameObject.GetComponent<Rigidbody>() != null &&*/ Vector3.Dot(col.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity) >= 0.0f && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop != 10)
        {
            tag = col.gameObject.transform.parent.gameObject.tag;
            if ((tag == "selfdrive" || tag == "Player" || i==1) && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision == 2)
            {
                if (col.gameObject.GetComponent<Rigidbody>().velocity.magnitude < this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
                {
                    this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().IdealSpeed2 = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude + 2.0f + 0.5f * (col.gameObject.GetComponent<Rigidbody>().velocity.magnitude - this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
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
            if ((tag == "selfdrive" || tag == "Player" || i == 1) && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().vision == 2)
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
