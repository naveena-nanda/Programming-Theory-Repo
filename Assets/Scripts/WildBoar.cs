using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildBoar : GreenBacks
{
    public int points { get; private set; }
   // private bool set = false;
    private Transform transform;
    
    // Start is called before the first frame update
    void Start()
    {
        //  transform.GetChild(0).gameObject.SetActive(false);

        speed = -1;
        transform = GetComponent<Transform>();
        strikeNumber = 8;
        stompNumber = 7;
        screamNumber = 7;
        points = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainManager.m_GameOver)
        {
            move(transform);
            DealHeroDamage(this.gameObject);
        }
        
    }

    private void OnMouseDown()
    {
        if (!mainManager.m_GameOver)
        {
            enemy = gameObject;
            resetButtons();
            setButtons();
            setUI();
        }
    }

}
