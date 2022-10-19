using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightTurn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    private int i;

    void Start()
    {
        i = 2;
    }

    public void OnPointerDown(PointerEventData eventdata)
    {
        i = 1;
    }
    public void OnPointerUp(PointerEventData eventdata)
    {
        i = 0;
    }
    void Update()
    {
        if (i == 1)
        {
            player.GetComponent<TestMainCar>().turn = 1.0f;
        }
        else if (i == 0)
        {
            player.GetComponent<TestMainCar>().turn = 0.0f;
            i = 2;
        }
    }
}