using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeCollider : MonoBehaviour
{
    public GameObject temp;
    public int tag;
    public float counter;
    public double factor;


    void Start()
    {
        temp = (GameObject)null;
        counter = 0.0f;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        counter += Time.fixedDeltaTime;
    }

    void OnTriggerStay(Collider col)
    {
        //double f = 0.2 + (1.0 / (double)(1.25+(0.005*(this.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().score))));
        //factor = f;
        if(this.gameObject.tag == "field of play0" && this.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().tag == tag && counter > 0.8f && this.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().arr.Count>0 && col.gameObject.tag == "roadguide")
        {
            //if (col.gameObject.GetComponent<RoadGuide>().j<3)
            {
                int random = UnityEngine.Random.Range(-17, 17);
                temp = this.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().arr[0];
                temp.transform.position = new Vector3((float) (col.gameObject.transform.position.x + ((random * Math.Sin(Mathf.Deg2Rad * col.gameObject.transform.eulerAngles.y)))), 1.5f, (float) (col.gameObject.transform.position.z + (random* Math.Cos(Mathf.Deg2Rad * col.gameObject.transform.eulerAngles.y))));
                temp.transform.rotation = Quaternion.Euler(new Vector3(0.0f, col.gameObject.transform.eulerAngles.y, 0.0f));
                temp.SetActive(true);
                this.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().arr.RemoveAt(0);
                this.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().outflow++;
                temp = (GameObject)null;
                counter = 0.0f;
                //this.gameObject.transform.parent.gameObject.GetComponent<fieldofplay>().tag++;
            }
        }
    }
}
