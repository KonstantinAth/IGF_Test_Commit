using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    [Header("Movement Configs")]
    [SerializeField] [Range(0, 200)] private float walkingMoveSpeed;
    [SerializeField] [Range(0, 400)] private float runningMoveSpeed;
    [SerializeField] [Range(0, 400)] private float rotationSpeed;
    [Header("Apply Gravity Configs")]
    [SerializeField] Transform bottomPosition;
    [SerializeField] float radius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float bodyMass;
    [SerializeField] float gravity = -9.81f;
    //Inputs
    bool isRunning => Input.GetKey(KeyCode.LeftShift);
    float inputX;
    float inputZ;
    float RotationY;
    //Movement Vectors
    Vector3 direction;
    Vector3 movement;
    Vector3 bodyVelocity;
    //References
    CharacterController playerController;
    // Start is called before the first frame update
    void Start() { Initialization(); }
    // Update is called once per frame
    void Update() {
        Movement();
        ApplyGravity();
    }
    void Initialization() { playerController = GetComponent<CharacterController>(); }
    //Store inputs...
    void Inputs() {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        RotationY = Input.GetAxis("Mouse X");
        direction = new Vector3(inputX, 0.0f, inputZ);
        //transform local space direction on the axis to world space... 
        movement = transform.TransformDirection(direction).normalized;
        //Rotate player on the Y axis...
        transform.Rotate(0.0f, RotationY * rotationSpeed * Time.deltaTime, 0.0f);
    }
    void Movement() {
        Inputs();
        //Check player's speed according to user input...
        if(isRunning) { playerController.Move(movement * runningMoveSpeed * Time.deltaTime); }
        else { playerController.Move(movement * walkingMoveSpeed * Time.deltaTime); }
    }
    void ApplyGravity() {
        if(IsGrounded()) { bodyVelocity.y = 0; }
        else {
            //Applying neutons law Äp = mass * gravity * time ^ 2 to measure drag...
            bodyVelocity.y += bodyMass * gravity * Mathf.Pow(Time.deltaTime, 2);
            //apply drag to the player...
            playerController.Move(bodyVelocity);
        }
    }
    bool IsGrounded(){
        if(Physics.CheckSphere(bottomPosition.position, radius, groundLayer)) { return true; }
        else { return false; }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(bottomPosition.position, radius);
    }
}
