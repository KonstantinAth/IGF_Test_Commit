using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Player - Object interactions...
public class PlayerObjectInteractions : MonoBehaviour
{
    [SerializeField] ObjectDetection objectDetection;
    [SerializeField] int maxLogsInPlayer = 3;
    private int hitPerClick = 1;
    //Store player's inputs...
    public bool playerChopsWood => Input.GetMouseButtonDown(0);
    public bool playerStoresWood => Input.GetKeyDown(KeyCode.E);
    //Take the needed references from the ObjectDetection.cs
    bool treeDetected => objectDetection.detectedTreeObject;
    bool storageDetected => objectDetection.detectedStorageObject;
    // Update is called once per frame
    void Update()
    {
        HandleInteractions();
    }
    //handle player - object interactions...
    void HandleInteractions()
    {
        HandleTreeInteraction();
        HandleStorageInteraction();
    }
    //Each time the player interacts with an object the object's hit tolerance decreases by one (TakeDamage),
    //and hit particles take the hitPoint's position & play...
    void HandleTreeInteraction()
    {
        if (treeDetected && objectDetection.PlayerNearTree() && playerChopsWood)
        {
            ParticlesManager._instance.hitPoitParticles.transform.position = objectDetection.GetHitPosition();
            ParticlesManager._instance.hitPoitParticles.Play();
            objectDetection.GetHitGameObject().GetComponent<IDestructable>().TakeDamage(hitPerClick);
        }
    }
    //Store player's woods & earn a coin for every wood...
    void HandleStorageInteraction()
    {
        if (storageDetected && playerStoresWood)
        {
            if (UIManager.logsInInventory.Equals(maxLogsInPlayer))
            {
                for (int i = 0; i < maxLogsInPlayer; i++)
                {
                    UIManager.coinsInInventory += 3;
                }
                UIManager.logsInInventory = 0;
            }
        }
    }
}