using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRot = 0f;
   
    public float xSen = 30f;
    public float ySen = 30f;

    // Start is called before the first frame update
    public void ProcessLook(Vector2 input) 
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calcular rotacion para arriba abajo
        xRot -= (mouseY * Time.deltaTime) * ySen;
        xRot = Mathf.Clamp(xRot, -80f, 80f);
        //usa quaterniones para la rotacion
        cam.transform.localRotation = Quaternion.Euler(xRot,0,0);
        //para que el jugador mire derecha e izquierda
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSen);
    }
}
