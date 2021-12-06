using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

    // Use this for initialization
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject explosionParticle;

    private MyGameManager myGameManager;

    void Start()
    {
        myGameManager = GameObject.Find("Game Manager").GetComponent<MyGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Hit!");
            myGameManager.UpdateScore(1);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, transform.rotation);

            if (audioClip)
            {
                if (gameObject.GetComponent<AudioSource>())
                {
                    //gameobject has audiosource
                    gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip, 1f);
                }
                else
                {
                    //add audiosource to gameobject: dynamically create object with audiosource,it will remove itself
                    AudioSource.PlayClipAtPoint(audioClip, transform.position, 1f);
                }
            }
        }
    }
}
