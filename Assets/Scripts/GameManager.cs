using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    End ce;

    CanvasManager currentCanvas;

    int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            currentCanvas = FindObjectOfType<CanvasManager>();

            _score = value;
            currentCanvas.SetScoreText();

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ce = gameObject.GetComponent<End>();
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) /*&& ce.canEnd==true*/)
        {
            QuitGame();
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
