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
	}

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(11.0f);
        SceneManager.LoadScene("Test LD");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
