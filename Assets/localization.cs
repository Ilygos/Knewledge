using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class localization : MonoBehaviour {

    public GameObject titleCard;
    //public Class x_Button;

    public Image titlePanel;
    public Sprite menuEn;
    public Sprite menuFr;
    public Sprite menuVn;
    public Sprite menuJp;
    public Sprite menuKr;

    private bool isEn;
    private bool isFr;
    private bool isVn;
    private bool isJp;
    private bool isKr;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(titleCard.activeInHierarchy)
        {
            if (isFr)
                titlePanel.sprite = menuFr;
            if (isEn)
                titlePanel.sprite = menuEn;
            if (isVn)
                titlePanel.sprite = menuVn;
            if (isJp)
                titlePanel.sprite = menuJp;
            if (isKr)
                titlePanel.sprite = menuKr;

            if (Input.GetButtonDown("Interaction0") || Input.GetButtonDown("Interaction1") || Input.GetButtonDown("Interaction2") || Input.GetButtonDown("Interaction3"))
                SceneManager.LoadScene("LoadingTuto");
        }
	}

    public void fr()
    {
        isFr = true;
        isEn = false;
        isVn = false;
        isJp = false;
        isKr = false;
    }

    public void en()
    {
        isFr = false;
        isEn = true;
        isVn = false;
        isJp = false;
        isKr = false;
    }

    public void vn()
    {
        isFr = false;
        isEn = false;
        isVn = true;
        isJp = false;
        isKr = false;
    }

    public void jp()
    {
        isFr = false;
        isEn = false;
        isVn = false;
        isJp = true;
        isKr = false;
    }

    public void kr()
    {
        isFr = false;
        isEn = false;
        isVn = false;
        isJp = false;
        isKr = true;
    }

}
