using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject endText;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetScoreText()
    {
        if (GameManager.instance)
        {
            scoreText.text = GameManager.instance.score.ToString();
        }
        else
        {
            SetScoreText();
        }
    }
    public void ShowEndText()
    {
        endText.SetActive(true);
    }

}
