using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidAutoController : MonoBehaviour {

    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        anim.SetFloat("InputZ", 1.0f, 0.0f, Time.deltaTime);
        anim.SetFloat("InputMagnitude", 1.0f, 0.0f, Time.deltaTime);
    }
}
