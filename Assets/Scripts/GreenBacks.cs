using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenBacks : MonoBehaviour
{
    protected int strikeNumber;
    protected int stompNumber;
    protected int screamNumber;

    protected Text strike;
    protected Text stomp;
    protected Text scream;

    protected Button strikeButton;
    protected Button stompButton;
    protected Button screamButton;

    protected bool listenersInit = false;

    protected GameObject enemy;

    public GameObject EnemyUI;
    public MainManager mainManager;

    protected float speed;

    public int rollValue;
    

    // Start is called before the first frame update
    void Awake()
    {
        mainManager = FindObjectOfType<MainManager>().GetComponent<MainManager>();
        EnemyUI = GameObject.Find("Enemy UI");

        strikeButton = EnemyUI.transform.GetChild(0).GetComponent<Button>();
        stompButton = EnemyUI.transform.GetChild(1).GetComponent<Button>();
        screamButton = EnemyUI.transform.GetChild(2).GetComponent<Button>();

        strike = EnemyUI.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        stomp = EnemyUI.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        scream = EnemyUI.transform.GetChild(2).GetChild(0).GetComponent<Text>();


        //   EnemyUI.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Strike()
    {
        rollValue = mainManager.GenerateRollValue();
        if (rollValue >= strikeNumber)
        {
            Debug.Log("enemy lost strike: " + rollValue + ">" + strikeNumber);
            Destroy(enemy);

            mainManager.biscuitPoints += 1;
            Debug.Log("BP"+ mainManager.biscuitPoints);
            mainManager.scoreText.text = "Score Text: " + mainManager.biscuitPoints;

        }
        else
        {
            Debug.Log("enemy won strike: " + rollValue + "<" + strikeNumber);

        }
        mainManager.DestroyActiveHeros();
    }

    protected virtual void Stomp()
    {
        rollValue = mainManager.GenerateRollValue();
        if (rollValue >= stompNumber)
        {
            Debug.Log("enemy lost stomp");
            Destroy(enemy);


            mainManager.biscuitPoints += 1;
            Debug.Log("BP" + mainManager.biscuitPoints);
            mainManager.scoreText.text = "Score Text: " + mainManager.biscuitPoints;

        }
        else
        {
            Debug.Log("enemy won stomp");

        }
        mainManager.DestroyActiveHeros();
    }

    protected virtual void Scream()
    {
        rollValue = mainManager.GenerateRollValue();
        if (rollValue >= screamNumber)
        {
            Debug.Log("enemy lost scream");
            Destroy(enemy);


            mainManager.biscuitPoints += 1;
            Debug.Log("BP" + mainManager.biscuitPoints);
            mainManager.scoreText.text = "Score Text: " + mainManager.biscuitPoints;


        }
        else
        {
            Debug.Log("enemy won scream");

        }
        mainManager.DestroyActiveHeros();
    }

    protected void setUI()
    {
             strike.text = "Strike: " + strikeNumber.ToString();
       stomp.text = "Stomp: " + stompNumber.ToString();
        scream.text = "Scream: " + screamNumber.ToString();
    }

    protected void setButtons()
    {
        strikeButton.onClick.AddListener(Strike);
        stompButton.onClick.AddListener(Stomp);
        screamButton.onClick.AddListener(Scream);
    }

    protected void resetButtons()
    {
        strikeButton.onClick.RemoveListener(Strike);
        stompButton.onClick.RemoveListener(Stomp);
        screamButton.onClick.RemoveListener(Scream);

    }

    protected virtual void move(Transform transform)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    protected void DealHeroDamage(GameObject gameObject)
    {
        if(transform.position.z < -13)
        {
            mainManager.maxHeroes -= 1;
            
            if (mainManager.maxHeroes <= 0)
            {
                Debug.Log("Max Heroes: " + mainManager.maxHeroes);
                mainManager.GameOver();
            }
            Destroy(gameObject);
            Debug.Log("enemy passed barrier");
            mainManager.GenerateEnemies(1);
        }
    }
}
