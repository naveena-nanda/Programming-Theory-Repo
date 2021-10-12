using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MenuUIHandler : MonoBehaviour
{
    private NamePicker namePicker;
    public InputField nameInput;
    public Text bestScore;
    // Start is called before the first frame update
    void Start()
    {
        namePicker = nameInput.GetComponent<NamePicker>();
        Manager.Instance.LoadName();
        nameInput.text = Manager.Instance.playerName;
        bestScore.text = "Best score: " + Manager.Instance.bestPlayer + " : " + Manager.Instance.bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewNameInput(string namePick)
    {
        Manager.Instance.playerName = namePick;
    }

    
    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
