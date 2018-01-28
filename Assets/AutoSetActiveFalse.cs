using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetActiveFalse : MonoBehaviour {

    ParticleSystem _particles;

    // Use this for initialization
    void OnEnable()
    {
        _particles = GetComponent<ParticleSystem>();
        _particles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_particles.isPlaying) gameObject.SetActive(false);
    }
}
