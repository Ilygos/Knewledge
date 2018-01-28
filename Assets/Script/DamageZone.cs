using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {

    public float timeBeforeIntactivity = 1.0f;

    public AudioSource _adSrc;

    // Use this for initialization
    void OnEnable () {
        StartCoroutine(timeBeforeInactive());
        _adSrc.Play();
	}
	
    IEnumerator timeBeforeInactive()
    {
        yield return new WaitForSeconds(timeBeforeIntactivity);
        GetComponentInParent<CharacterController2D>().isDashing = false;
        gameObject.SetActive(false);
    }

}
