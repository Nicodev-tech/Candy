using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caramelo : MonoBehaviour
{
    public float timeOut = 2f;
    [SerializeField] private AudioClip carameloSound;
    private float timer;
    private void OnCollisionEnter(Collision collision) 
    { 
        Transform  hitTransform = collision.transform;
        SoundFXManager.Instance.PlaySoundFXClip(carameloSound, collision.transform, 0.3f);
        
    }
    void SendAIToCaramelo()
    {
        GameObject[] nenes = GameObject.FindGameObjectsWithTag("Nenes");
        foreach (var nene in nenes)
        {
            Enemy Nene = nene.GetComponent<Enemy>();
            Nene.Caramelo = gameObject;
        }
    }
    private void Awake()
    {
        SendAIToCaramelo();
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
