using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class RoadGuide : MonoBehaviour
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
    public int j = 0;
    public float i;
    public int z;
    private GameObject refer;
    // Start is called before the first frame update
    void Start()
    {
        refer = GameObject.FindGameObjectsWithTag("Player")[0];
        i = 21.0f;
        j = 0;
    }

    void FixedUpdate()
    {
        i += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Bumper")
        {
            //j = 1;
            j++;
            if (col.transform.parent.gameObject.GetComponent<SelfDrive>().veto == 0)
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

                if (Math.Abs(deviation) < 50.0f)
                {
                    col.transform.parent.gameObject.GetComponent<SelfDrive>().deviation = deviation;
                    col.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 9;
                    col.transform.parent.gameObject.GetComponent<SelfDrive>().turn = 0.0f;
                    col.transform.parent.gameObject.GetComponent<SelfDrive>().i = 0;
                    j = 1;

                    diff2.y = 90.0f - diff2.y;
                    diff2.y = Mathf.Deg2Rad * diff2.y;
                    pos1 = col.transform.parent.gameObject.transform.position;
                    pos2 = this.gameObject.transform.position;
                    if (col.transform.parent.gameObject.GetComponent<SelfDrive>().PivotOffsetmag != 0.0f)
                    {
                        pos1 = pos1 + new Vector3((float)((Math.Sin((col.transform.parent.gameObject.GetComponent<SelfDrive>().PivotOffsetdir + diff1.y) * Mathf.Deg2Rad)) * (col.transform.parent.gameObject.GetComponent<SelfDrive>().PivotOffsetmag)), 0.0f, (float)((Math.Cos((col.transform.parent.gameObject.GetComponent<SelfDrive>().PivotOffsetdir + diff1.y) * Mathf.Deg2Rad)) * (col.transform.parent.gameObject.GetComponent<SelfDrive>().PivotOffsetmag)));
                    }

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
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Bumper") && (j == 1) && (col.transform.parent.gameObject.GetComponent<SelfDrive>().veto == 0))
        {
            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().deviation = 0.0f;
            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().diff1y = 0.0f;
            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().diff2y = 0.0f;
            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().displacement = 0.0f;
            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().i = 1;
            //j = 0;
            j--;
        }
    }
}
