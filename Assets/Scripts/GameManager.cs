using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Credit;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = Credit.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Credit.activeSelf==true)
        {
            if (!anim.isPlaying)
            {
                Credit.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Credit.SetActive(false);
            }
        }
    }
    public void uiPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void uiOpt()
    {

    }

    public void uiCredit()
    {
        Credit.SetActive(true);
        
    }

    public void uiQuit()
    {
        Application.Quit();
    }

}
