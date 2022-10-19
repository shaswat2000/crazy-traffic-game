using System.Collections;
using System.Collections.Generic;
using System.Configuration;
//using System.Runtime.Remoting.Channels;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    
    public int veto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        veto = 0;
    }
    void OnTriggerStay(Collider col)
    {
        if(this.transform.parent.gameObject.GetComponent<SelfDrive>().signal == col.gameObject.tag)
        {
            if(this.transform.parent.gameObject.GetComponent<SelfDrive>().veto <=1)
            {
                this.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 1;
                veto = 1;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "roadguide")
        {
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().deviation = 0.0f;
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().displacement = 0.0f;
        }
    }
    void LateUpdate()
    {
        if ((veto == 0) && (this.transform.parent.gameObject.GetComponent<SelfDrive>().stop == 10))
        {
            this.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 0;
            this.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 0;
        }
    }
}
