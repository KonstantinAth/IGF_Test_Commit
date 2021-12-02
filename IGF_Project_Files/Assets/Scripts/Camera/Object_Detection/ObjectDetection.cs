using UnityEngine;
public class ObjectDetection : MonoBehaviour {
    Camera mainCamera;
    [Header("Raycast Data")]
    [SerializeField] PlayerMovement player;
    [SerializeField] LayerMask treeLayerMask;
    [SerializeField] LayerMask storageLayer;
    [SerializeField] float maxDistance;
    [SerializeField] float storageDistance;
    [Header("UI Indication")]
    [SerializeField] UIManager uiManager;
    public bool detectedTreeObject;
    public bool detectedStorageObject;
    RaycastHit TreeHit;
    Vector3 hitTransform;
    GameObject hitGameObject;
    bool playerNearTree;
    Ray CameraRay;
    Ray PlayerRay;
    void Initialization() {
        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start() { Initialization(); }
    // Update is called once per frame
    void FixedUpdate() {
        DetectTree();
        DetectStorage();
        HandleInventoryFull();
    }
    //Detect tree objects...
    void DetectTree() {
        CameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        detectedTreeObject = Physics.Raycast(CameraRay, out TreeHit, maxDistance, treeLayerMask);
        if (!detectedStorageObject) {
            if (detectedTreeObject) {
                hitTransform = TreeHit.point;
                hitGameObject = TreeHit.rigidbody.gameObject;
                playerNearTree = hitGameObject.GetComponent<SingleTree>().PlayerNearTree();
                if (playerNearTree)
                {
                    uiManager.SetBackgroundState(true);
                    uiManager.SetInstructions("Trees");
                }
            }
            else {
                uiManager.SetBackgroundState(false);
                return;
            }
        }
    }
    //Detect Storage object...
    bool DetectStorage() {
        PlayerRay = new Ray(player.transform.position, player.transform.forward);
        detectedStorageObject = Physics.Raycast(PlayerRay, storageDistance, storageLayer);
        if (!detectedTreeObject) {
            if (detectedStorageObject) {
                uiManager.SetBackgroundState(true);
                uiManager.SetInstructions("Storage");
                return true;
            }
            else {
                uiManager.SetBackgroundState(false);
            }
        }
        return false;
    }
    //Handle inventory full case...
    void HandleInventoryFull() {
        if (!detectedStorageObject && !detectedTreeObject) {
            if (UIManager.logsInInventory >= 3) {
                uiManager.SetBackgroundState(true);
                uiManager.SetInstructions("Inventory Full");
            }
            else { uiManager.SetBackgroundState(false); }
        }
    }
    //Hit object info...
    public Vector3 GetHitPosition() {
        if(TreeHit.rigidbody != null) { return hitTransform; }
        else { return default; }
    }
    public GameObject GetHitGameObject() {
        if (TreeHit.rigidbody != null) { return hitGameObject; }
        else { return null; }
    }
    public bool PlayerNearTree() { return playerNearTree; }
}
