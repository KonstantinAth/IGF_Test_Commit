using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Log : MonoBehaviour
{
    [Header("Move Towards player speed")]
    [SerializeField] float moveSpeed;
    [Header("Initial force to be added")]
    [SerializeField] float momentumPulse;
    [Header("Minimum distance to move towards player")]
    [SerializeField] float minPlayerDistance = 5.0f;
    PlayerMovement player;
    Collider thisCollider;
    Rigidbody rb;
    int logsInPlayer;
    // Start is called before the first frame update
    void OnEnable() {
        Initialization();
    }
    // Update is called once per frame
    void Update() { MoveTowardsPlayer(); }
    void Initialization() {
        player = FindObjectOfType<PlayerMovement>();
        thisCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        InitialForce();
    }
    //Initial Impulse force to be added when the object is enabled...
    void InitialForce() {
        rb.AddForce(Vector3.up * Random.Range(-momentumPulse, momentumPulse) * Time.deltaTime, ForceMode.Impulse);
        rb.AddForce(Vector3.right * Random.Range(-momentumPulse, momentumPulse) * Time.deltaTime, ForceMode.Impulse);
        rb.AddForce(Vector3.forward * Random.Range(-momentumPulse, momentumPulse) * Time.deltaTime, ForceMode.Impulse);
    }
    //Move towards player when at a certain distance...
    void MoveTowardsPlayer() {
        logsInPlayer = UIManager.logsInInventory;
        float Distance = Vector3.Distance(transform.position, player.transform.position);
        if (logsInPlayer < 3 && Distance <= minPlayerDistance) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            if (Distance <= 1f) {
                thisCollider.isTrigger = true;
            }
        }
    }
    //When player enters trigger increase the number of logs the player has on his possession & destroy current object...
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            UIManager.logsInInventory++;
            Destroy(this.gameObject);
        }
    }
}
