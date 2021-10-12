using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public Color heroColor;
    public int heroNumber;
    public int heroPositionIndex;
    public KeyCode heroKeyCode;
    private bool picked = false;

    public MainManager mainManager;
  //  public Text heroText;
    // Start is called before the first frame update
    void Start()
    {
        mainManager = FindObjectOfType<MainManager>().GetComponent<MainManager>();
        heroKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), (heroPositionIndex+48).ToString()); ;
        Debug.Log(heroKeyCode);
    }

    // Update is called once per frame
    void Update()
    {
       
        MoveTo(new Vector3(-10 + 2*heroPositionIndex , 0, -15));
        if (!mainManager.m_GameOver)
        {
            if (Input.GetKeyDown(heroKeyCode))
            {
                Debug.Log(heroKeyCode);
                if (!picked)
                {
                    mainManager.activesHeroes.Add(this);
                    transform.GetChild(0).gameObject.SetActive(true);
                    picked = true;
                    //  Debug.Log(mainManager.activesHeroes);
                }
                else if (picked)
                {
                    picked = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                    mainManager.activesHeroes.Remove(this);
                }
                // Debug.Log("HPI: " + heroPositionIndex);
            }
        }
        
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 10);
    }

    public void OnMouseDown()
    {
        
        if (!picked)
        {
            mainManager.activesHeroes.Add(this);
            transform.GetChild(0).gameObject.SetActive(true);
            picked = true;
          //  Debug.Log(mainManager.activesHeroes);
        }
        else if(picked)
        {
            picked = false;   
            transform.GetChild(0).gameObject.SetActive(false);
            mainManager.activesHeroes.Remove(this);
        }
    }
    

}
