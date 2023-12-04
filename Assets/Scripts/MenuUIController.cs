using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif


[DefaultExecutionOrder(1000)]

public class MenuUIController : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button startButton;
    public Button resetButton;
    public TMP_Text scoresText;
    public TMP_Text scoreNamesText;
    // Start is called before the first frame update
    void Start()
    {
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputField.text = DataManager.Instance.lastUsername;
        int index = 0;
        foreach (string name in DataManager.Instance.userScoreNames.AsEnumerable().Reverse())
        {
            scoreNamesText.text += (++index).ToString() + " " + name + "\n";
            if (index > 9) break;
        }
        index = 0;
        foreach (int score in DataManager.Instance.userScores.AsEnumerable().Reverse())
        {
            index++;
            scoresText.text += score + "\n";
            if (index > 9) break;
        }
    }

    private void ValueChangeCheck()
    {
        string username = inputField.text;
        if (username.Length >= 3 && username.Length <= 8)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetScoreBoard()
    {
        DataManager.Instance.userScoreNames = new List<string>();
        DataManager.Instance.userScores = new List<int>();
        DataManager.Instance.SaveUserScoresData();
        scoresText.text = "";
        scoreNamesText.text = "";
    }

    public void StartNew()
    {
        DataManager.Instance.lastUsername = inputField.text;
        DataManager.Instance.SaveLastUserData();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
