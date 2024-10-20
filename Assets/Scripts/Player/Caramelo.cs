using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caramelo : MonoBehaviour
{
    public float timeOut = 2f;
    private float timer;
    private void OnCollisionEnter(Collision collision) 
    { 
        Transform  hitTransform =collision.transform;
        if (hitTransform.CompareTag("Nenes")) 
        {
            Debug.Log("le pegamos al nene, no para");
            Destroy(gameObject);

        }
        
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeOut)
        {
            Destroy(gameObject);
        }
    }
}
