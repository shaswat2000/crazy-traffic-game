using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vision : MonoBehaviour
{
    public string tag;
    public float i;
    public Collider a;
    void Start()
    {
        i = 0.0f;
        a = (Collider)null;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        i -= Time.deltaTime;
        if ((i < 0.0f) && (this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop != 1))
        {
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().brake = 0.0f;
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 0;
            i = -1.0f;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "selfdrive")
        {
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().brake = 1.0f;
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().veto = 3;
            i = 1.0f;
            a = col;
        }
    }
}
