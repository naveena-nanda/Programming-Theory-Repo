using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawner;
    public GameObject hero;
    public GameObject boar;

    public Text scoreText;
    public GameObject gameOverText;

    public bool actionTaken = false;
    public bool[] places = new bool[9];
    public bool m_GameOver = false;

    Vector3 spawnPosition;

    public Color[] colors;

    public int biscuitPoints;
    public int maxHeroes = 8;

    List<GameObject> heroes = new List<GameObject>();
    public List<Hero> activesHeroes = new List<Hero>();
    public GameObject[] enemyOptions = new GameObject[3];

    
    

    void Start()
    {
        gameOverText.SetActive(false);
        spawnPosition = spawner.transform.position;
        scoreText.text = "Score: 0";
        Instantiate(boar, new Vector3(-8, -1, -5), boar.transform.rotation);
        GenerateStartingBoard();
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_GameOver)
            {


                GenerateHero();

            }
            else if (m_GameOver)
            {
                Debug.Log("game over is true");

                SceneManager.LoadScene(0);


            }
        }
    }

    //Hero functions
    public void GenerateHero()
    {
        if(heroes.Count < maxHeroes && actionTaken == false)
        {
            GameObject newHero = Instantiate(hero, spawnPosition, hero.transform.rotation);
            Hero newHeroScript = newHero.GetComponent<Hero>();

            newHeroScript.heroColor = GenerateRandomColor();
            newHero.GetComponent<Renderer>().material.color = newHeroScript.heroColor;

            newHeroScript.heroNumber = Random.Range(1, 12);

            heroes.Add(newHero);
            for(int i = 0; i < maxHeroes; i++)
            {
                if(places[i] == false)
                {
                    newHeroScript.heroPositionIndex = i;
                    places[i] = true;
                    break;
                }
            }
           // newHeroScript.heroPositionIndex = heroes.Count; //gives one for first
          //  Debug.Log(newHeroScript.heroPositionIndex);
        }
        else
        {
            Debug.Log("Too many heroes");
        }

    }

    private Color GenerateRandomColor()
    {
        int randomInt = Random.Range(0, colors.Length);
        return colors[randomInt];
    }

    private void GenerateStartingBoard()
    {
        for(int i = 0; i < 5; i++)
        {
            GenerateHero();
        }
    }

    public int GenerateDiceNumber()
    {
        if(activesHeroes.Count == 0)
        {
            return 1;
        }
        if(activesHeroes.Count > 0 && checkMatchesInHand() > 0)
        {

            return checkMatchesInHand();
        }

        return 0;
    }

    private int checkMatchesInHand()
    {
        Color heroHandColor = activesHeroes[0].heroColor;
     //   Debug.Log(heroHandColor);
        int count = 0;
        foreach(Hero i in activesHeroes)
        {
            if(i.heroColor == heroHandColor)
            {
                count++;
            }
            else
            {
                count = 0;
                Debug.Log("invalid hand");
            }
        }

        return count;
    }

    public int GenerateRollValue()
    {
        int rollValue = 0;
        int diceNumber = GenerateDiceNumber();
        Debug.Log(diceNumber);
        // int rolledDice = 0;

        for (int i = 0; i < diceNumber; i++)
        {
            int randomInt = Random.Range(1, 6);
            rollValue += randomInt;
            Debug.Log("Dice roll: " + rollValue);

        }
        return rollValue;
    }

    public void DestroyActiveHeros()
    {

          foreach (Hero i in activesHeroes)
       {
            Destroy(i.gameObject);
            places[i.heroPositionIndex] = false;
            heroes.Remove(i.gameObject);
       }
        activesHeroes.Clear();
    }


    public void GenerateEnemies(int numberOfEnemies)
    {
        for(int i = 0;i < numberOfEnemies; i++)
        {
            GameObject currentEnemy = enemyOptions[Random.Range(0, enemyOptions.Length)];
            Vector3 spawnPos = new Vector3(Random.Range(-14, 5), -1, 3);
            Instantiate(currentEnemy, spawnPos, currentEnemy.transform.rotation);
        }
    }

    public void GameOver()
    {
        if(biscuitPoints > Manager.Instance.bestScore)
        {
            Manager.Instance.bestScore = biscuitPoints;
            Manager.Instance.bestPlayer = Manager.Instance.playerName;
            scoreText.text = "Best Score: " + Manager.Instance.bestPlayer + " : " + Manager.Instance.bestScore;
            Manager.Instance.SaveName();
        }
        m_GameOver = true;
        gameOverText.SetActive(true);
    }

}
