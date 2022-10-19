using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    public int score;
    public Text t;
    public GameObject a;
    public Button restart;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        restart.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (a.activeSelf)
        {
            score += (int)((0.0 * Time.deltaTime) + (0.1* a.GetComponent<Rigidbody>().velocity.magnitude));
            t.text = "Current Score: " + score;
        }
        else
        {
            t.text = "Final Score: " + score;
            restart.gameObject.SetActive(true);
            quit.gameObject.SetActive(true);
        }
    }
}
