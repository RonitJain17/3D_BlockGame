using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager manager;
    public float moveSpeed;
    private float maxSpeed = 5f;
    private Vector3 input;
    private Vector3 spawn;
    public GameObject deathPartiles;
    public bool usesManager = true;
    public AudioClip[] audioClip;


    
    void Start()
    {
        spawn = transform.position;
        if(usesManager){
            manager = manager.GetComponent<GameManager>();
        }
    }

        void FixedUpdate()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        
        if(GetComponent<Rigidbody>().velocity.magnitude<maxSpeed){
            GetComponent<Rigidbody>().AddRelativeForce(input*moveSpeed);
        }

        if(transform.position.y < -2){
            Die();
        }
        
    }

    void OnCollisionEnter(Collision other){
        if(other.transform.tag == "Enemy"){
            Die();
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.transform.tag == "Goal"){
            PlaySound(1);
            Time.timeScale = 0f;
            manager.CompleteLevel();
        }

        if(other.transform.tag == "Enemy"){
            PlaySound(3);
            Die();
        }

        if(other.transform.tag == "Wall"){
            PlaySound(2);
        }

        if(other.transform.tag == "Token"){
            if(usesManager){
                manager.tokenCount += 1;
            }
            PlaySound(0);
            Destroy(other.gameObject);
        }
    }

    void PlaySound(int clip){
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }

    public void Die(){
        Instantiate(deathPartiles,transform.position,Quaternion.Euler(270,0,0));
        transform.position = spawn;
    }


}
