using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ShipController : MonoBehaviour
{

    [SerializeField] private float forwardSpeed = 25f, strafeSpeed = 7.5f;
    private float activeForwardSpeed, activeStrafeSpeed;
    [SerializeField] private float forwardAcceleration = 2.5f, strafeAcceleration = 2f;

    [SerializeField] private float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private Rigidbody body;
    float deadZone = 0.1f;
    private int layerMask;
    [SerializeField] private float hoverHeight = 5f;
    [SerializeField] private float hoverForce = 9f;
    [SerializeField] private GameObject[] hoverPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        layerMask = 1 << LayerMask.NameToLayer("Player"); // Get the layer mask of the player
        layerMask = ~layerMask; // Invert to get everything except the player

        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        Stearing();
    }
    private void FixedUpdate()
    {
        Hover();
    }

    private void Stearing() // https://www.youtube.com/watch?v=J6QR4KzNeJU
    {
        // Mouse look
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        transform.Rotate(0f, mouseDistance.x * lookRotateSpeed * Time.deltaTime, 0f, Space.Self);

        // Button presses
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
    }


    // https://www.youtube.com/watch?v=5B6ALcOX4b8
    private void StearingHover()
    {

    }

    private void Hover()
    {
        RaycastHit hit;

        for(int i = 0; i < hoverPoints.Length; i++)
        {
            var hoverPoint = hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask))
            {
                Debug.Log("Hover hit");
                body.AddForceAtPosition(Vector3.up * hoverForce * (1f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
            }
            else
            {
                if(transform.position.y > hoverPoint.transform.position.y)
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * -hoverForce, hoverPoint.transform.position);
                }
                else
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * -hoverForce, hoverPoint.transform.position);
                }
            }   
        }
    }
}
