﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fopEdges : MonoBehaviour
{
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
        if(col.gameObject.tag == "selfdrive")
        {
            Destroy(col.transform.parent.gameObject);
        }
    }
    /*void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "selfdrive")
        {
            Destroy(col.transform.parent.gameObject);
        }
    }*/
}
