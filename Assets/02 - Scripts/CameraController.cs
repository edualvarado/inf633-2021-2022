using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float pan_sensitivity = 2.0f;
    public float look_sensitivity = 2.0f;
    public float scroll_sensitivity = 50.0f;

    private float rot_x;
    private float rot_y;

    void Start () {
        Vector3 eulers = transform.localEulerAngles;
        rot_x = eulers.x;
        rot_y = eulers.y;
    }

    void Update () {
        // Mimic editor camera
        // if (Camera.current) {
        //     Transform t = Camera.current.transform;
        //     transform.position = t.position;
        //     transform.rotation = t.rotation;
        // }

        Transform t = transform;
        float mx = Input.GetAxisRaw("Mouse X");
        float my = Input.GetAxisRaw("Mouse Y");
        // float hztl = Input.GetAxis("Horizontal");
        // float vtcl = Input.GetAxis("Vertical");
        float scrl = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(2)) {
            t.position -= (t.right * mx + t.up * my) * pan_sensitivity;
        } else if (Input.GetMouseButton(1)) {
            rot_x -= my * look_sensitivity;
            rot_y += mx * look_sensitivity;
        }

        if (scrl != 0) {
            t.position += t.forward * scrl * scroll_sensitivity;
        }

        t.localRotation = Quaternion.Euler(rot_x, rot_y, 0.0f);
    }
}
