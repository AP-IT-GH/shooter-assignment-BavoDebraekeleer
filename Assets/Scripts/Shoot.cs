using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject projectileLight;

    private bool isShooting = false;
    private float timer = 10f;
    private bool start = false;
    [SerializeField] private float shootRate = 3f;
    [SerializeField] private float projectileSpeed = 100f;
    private bool isShootingLeft = true;

    [SerializeField] private AudioClip audioClip;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting && timer >= shootRate)//shoot{
        {
            isShooting = false;
            Sound();

            if (isShootingLeft)
            {
                CreateProjectile(-0.40f, -1.5f);
                isShootingLeft = false;
            }
            else
            {
                CreateProjectile(0.40f, -1.5f);
                isShootingLeft = true;
            }
            
            start = true;
            timer = 0f;
        }

        if (start)
        {
            if (timer < shootRate)
                timer += Time.deltaTime;
            else
            {
                timer = shootRate;
                start = false;
            }

        }

        
    }
    private void OnFire(InputValue value)
    {
        isShooting = true;
        Debug.Log("Fire!");
    }

    private void CreateProjectile(float x = 0f, float y = 0f, float forwardMultiplier = 4f)
    {
        GameObject newProjectile = Instantiate(projectile, transform.position + (transform.forward * forwardMultiplier) + new Vector3(x, y, 0f), transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }

    private void Sound()
    {
        if (audioClip)
        {
            if (gameObject.GetComponent<AudioSource>())
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip);
            }
            else
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.5f);
            }
        }
    }
}
