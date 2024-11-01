using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public Camera cam;
    private Vector3 playerVelocity;
    public float speed = 5f;
    //posicion de la camara cuando el personaje esta parado
    public float standingHeight = 0.6f;
    //la cantidad de unidades que baja el caracter
    public float crouchedHeight = 0.4f;
    [SerializeField] private AudioClip footStepSound;
    float footSoundTimer = 1f;
    float diferenceHeight = 0f;

   

    private bool IsGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 9f;

    bool crouching = false;
    float crouchTimer = 1f;
    bool lerpCrouch = false;
    bool sprinting = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        diferenceHeight = standingHeight - crouchedHeight;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = controller.isGrounded;
        
        if (lerpCrouch) {
            crouchTimer += Time.deltaTime;
            if (crouching)
            {
                cam.transform.localPosition = new Vector3(0, Mathf.Lerp(cam.transform.localPosition.y, diferenceHeight, 0.02f),0);
            }
            else 
            {
                cam.transform.localPosition = new Vector3(0, Mathf.Lerp(cam.transform.localPosition.y, standingHeight, 0.02f), 0);
            }
            //0.001f lo llamamos el valor aproximado pues cuando la posicion llega a 5.99f directamente tomamos como si hubiera llegado al destino
            if (crouchTimer >= 1 && (cam.transform.localPosition.y >= standingHeight - 0.01f || cam.transform.localPosition.y >= diferenceHeight - 0.01f))
               
            {
               float y = cam.transform.localPosition.y >= standingHeight - 0.01f ? standingHeight : diferenceHeight;
               cam.transform.localPosition = new Vector3(0, y, 0);
               lerpCrouch = false;
               crouchTimer = 0f;
            }
            crouchTimer = Mathf.Clamp(crouchTimer, 0f, 1f);
            Debug.Log(cam.transform.localPosition.y);
        }
    }
    //recive las inputs de inputmanager.cs y se las aplica al character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (IsGrounded && playerVelocity.y < 0) 
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        if (moveDirection.magnitude > 0f)
        {
            footSoundTimer += Time.deltaTime;
            if (footSoundTimer >= 0.8f)
            {
                
                SoundFXManager.Instance.PlaySoundFXClip(footStepSound, transform, 0.3f);
                footSoundTimer = 0f;
            }
            footSoundTimer = Mathf.Clamp(footSoundTimer, 0f, 0.8f);

        }
        //Debug.Log(playerVelocity.y);
    }
    public void Jump()  
    {
        if (IsGrounded) {
            
            playerVelocity.y = Mathf.Sqrt( jumpHeight * -3f * gravity);
            
        }
    }
    public void Crouch() 
    { 
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }
    public void Sprint() 
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = speed * 2;
        }
        else
        {
            speed = speed / 2;
        }
    }
}
