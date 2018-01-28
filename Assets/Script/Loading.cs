using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    public Image tutoPanno;

    public Sprite tuto1;
    public Sprite tuto4;
    public Sprite tuto3;
    public Sprite tuto2;


    // Use this for initialization
    void Start () {
		StartCoroutine(startGame());
		StartCoroutine(tuto1display());
	}

	IEnumerator tuto1display()
    {
        tutoPanno.sprite = tuto1;
        yield return new WaitForSeconds(3);
        StartCoroutine(tuto2display());
    }

    IEnumerator tuto2display()
    {
        tutoPanno.sprite = tuto2;
        yield return new WaitForSeconds(3);
        StartCoroutine(tuto3display());
    }

    IEnumerator tuto3display()
    {
        tutoPanno.sprite = tuto3;
        yield return new WaitForSeconds(3);
        StartCoroutine(tuto4display());
    }

    IEnumerator tuto4display()
    {
        tutoPanno.sprite = tuto4;
        yield return new WaitForSeconds(3);
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(12.0f);
        SceneManager.LoadScene("Test LD");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
