using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 200f, strafeSpeed = 200f;
    private float activeForwardSpeed, activeStrafeSpeed = 0f;
    [SerializeField] private float forwardAcceleration = 50f, strafeAcceleration = 50f;
    private Vector3 direction = Vector3.zero;

    [SerializeField] private float lookRotateSpeed = 20f;
    private Vector2 lookInput, screenCenter, mouseDistance = Vector2.zero;

    [SerializeField] private float rollSpeed = 90f, rollAcceleration = 3.5f;
    private float rollInput, rollActive = 0f;

    private Rigidbody body;

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Look & Roll
        rollActive = Mathf.Lerp(rollActive, rollInput, rollAcceleration * Time.deltaTime);

        //body.MoveRotation(Quaternion.Euler(new Vector3(-mouseDistance.y * lookRotateSpeed * Time.deltaTime,
        //    mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollActive * rollSpeed * Time.deltaTime)));
        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, 
            mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollActive * rollSpeed * Time.deltaTime, Space.Self);

        // Move
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, direction.z * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, direction.x * strafeSpeed, strafeAcceleration * Time.deltaTime);

        //body.AddForce(transform.forward * activeForwardSpeed * Time.deltaTime);
        //body.AddForce(transform.right * activeStrafeSpeed * Time.deltaTime);
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
    }

    private void OnMove(InputValue value)
    {
        var data = value.Get<Vector2>();
        direction = new Vector3(data.x, 0f, data.y);
    }

    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
    }

    private void OnRoll(InputValue value)
    {
        rollInput = value.Get<float>();
        Debug.Log(rollInput);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isDead = true;
        Debug.Log("Player died!");
    }
}
