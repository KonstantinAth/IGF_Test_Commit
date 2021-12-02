using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Single tree class derives from the IDestructable Interface indicating the object can be destructe
//It contains two methods that handle damage & tolerance point...
public class SingleTree : MonoBehaviour, IDestructable {
    [SerializeField] private int hitTolerance = 5;
    [SerializeField] GameObject logsLeftBehind;
    [SerializeField] int timesToInstantiateLeftOvers = 3;
    [SerializeField] float minDistance;
    [SerializeField] PlayerMovement playerMovement;
    public int currentPoints;
    // Start is called before the first frame update
    void Start() { Init(); }
    // Update is called once per frame
    void Update() {  HandleStatePointsLost(); }
    void Init() {
        currentPoints = hitTolerance;
    }
    void HandleStatePointsLost() {
        if(currentPoints <= 0) {
            SpawnAndDestroy();
        }
    }
    public int GetStatePoints() { return currentPoints; }
    public void TakeDamage(int damageReceived) {
        currentPoints -= damageReceived;
        if(currentPoints < 0) {
            currentPoints = 0;
        }
    }
    //Called when the object's tolerance point are equal to zero...
    void SpawnAndDestroy() {
        //Destruction particles take the object's position & play
        ParticlesManager._instance.destuctedTreesParticles.transform.position = transform.position;
        ParticlesManager._instance.destuctedTreesParticles.Play();
        //The needed logs/objects are instantiated...
        for (int i = 0; i < timesToInstantiateLeftOvers; i++) {
            Instantiate(logsLeftBehind, transform.position, Quaternion.identity);
        }
        //And we destroy the current object...
        Destroy(gameObject);
    }
    //Returns true if player is near a certain distance & false if he/she is not...
    public bool PlayerNearTree() {
        float distance = Vector3.Distance(transform.position, playerMovement.transform.position);
        if(distance <= minDistance) {
            return true;
        }
        return false;
    }
}
