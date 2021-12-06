using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private float lifeTime = 3f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < lifeTime)
            timer += Time.deltaTime;
        else
        {
            //after 3 seconds destroy this gameobject
            Destroy(gameObject);
        }
    }
}
