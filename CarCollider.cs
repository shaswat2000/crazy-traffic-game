using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{

    public int safe;
    public int i;
    public GameObject a;
    public GameObject b;
    void Start()
    {
        safe = 1;
        i = 0;
        a = (GameObject)null;
        if (this.gameObject.transform.parent.gameObject.tag == "Player")
            i = 1;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        //a = col.gameObject;
        if (i==1 && col.gameObject.tag == "selfdrive")
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
            b.SetActive(false);
        }

        if (col.gameObject.tag == "Respawn" && this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop != 11)
        {
            GameObject temp = this.gameObject.transform.parent.gameObject;
            //col.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().temp = temp;
            col.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().arr.Add(temp);
            col.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().inflow++;
            this.gameObject.transform.parent.gameObject.SetActive(false);
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().stop = 0;
            this.gameObject.transform.parent.gameObject.GetComponent<SelfDrive>().brake = 0.0f;
            temp = new GameObject(null);
            safe = 1;
        }
    }
}
