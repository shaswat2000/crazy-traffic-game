using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideDriveR : MonoBehaviour
{
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        counter = counter + Time.deltaTime;
        if (counter > 28)
        {
            counter = 0.0f;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (this.gameObject.tag == "signal1r")
        {
            if ((counter > 5) || (counter < 2))
            {
                if (col.gameObject.tag == "selfdrive")
                {
                    
                    if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue < 4)
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
                        if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue == 2)
                        {
                            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().orientation = 1;
                        }
                    }

                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 28 - counter;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal1r";
                }
            }
            else
            {
                
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 0.0f;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal1r";
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
            }
        }
        else if (this.gameObject.tag == "signal2r")
        {
            if ((counter < 9) || (counter > 12))
            {
                if (col.gameObject.tag == "selfdrive")
                {
                    
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal2r";
                    if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue < 4)
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
                        if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue == 2)
                        {
                            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().orientation = 2;
                        }
                    }

                    if (counter < 7)
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 7 - counter;
                    }

                    else
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 35 - counter;
                    }
                }
            }
            else
            {
               
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 0.0f;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal2r";
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
            }
        }
        else if (this.gameObject.tag == "signal3r")
        {
            if ((counter < 16) || (counter > 19))
            {
                if (col.gameObject.tag == "selfdrive")
                {
                    
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal3r";
                    if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue < 4)
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
                        if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue == 2)
                        {
                            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().orientation = 1;
                        }
                    }

                    if (counter < 7)
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 14 - counter;
                    }

                    else
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 42 - counter;
                    }
                }
            }
            else
            {
                
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 0.0f;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal3r";
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
            }
        }
        else if (this.gameObject.tag == "signal4r")
        {
            if ((counter < 23) || (counter > 26))
            {
                if (col.gameObject.tag == "selfdrive")
                {
                    
                    if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue < 4)
                    {
                        col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
                        if (col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue == 2)
                        {
                            col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().orientation = 2;
                        }
                    }

                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 21 - counter;
                    col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal4r";
                }
            }
            else
            {
                
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 1;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().StopTime = 0.0f;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 2;
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().signal = "signal4r";
                col.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().turnValue = UnityEngine.Random.Range(2, 4);
            }
        }
    }
}
