using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldofplay : MonoBehaviour
{
    public GameObject canvas;
    public GameObject temp;
    public GameObject curr;
    public List<GameObject> arr;
    public int info;
    public float counter;
    public int tag;
    public int inflow;
    public int outflow;
    public int score;

    void Start()
    {
        List<GameObject> arr = new List<GameObject>();
        info = 0;
        counter = 0.0f;
        temp = (GameObject)null;
        curr = (GameObject)null;
        inflow = 0;
        outflow = 0;
        tag = 0;
    }

    void FixedUpdate()
    {
        //score = canvas.GetComponent<Score>().score;
        if((info==0) && (arr.Count>0))
        {
            curr = arr[0];
            info = 1;
            arr.RemoveAt(0);
            outflow++;
        }
        if(tag>3)
        {
            tag = 0;
        }
    }

}
