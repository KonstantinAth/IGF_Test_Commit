using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
//UI Manager is responsible for the UI Elements & methods regarding them...
public class UIManager : MonoBehaviour {
    [System.Serializable]
    struct Instructions {
        public string instructionName;
        [TextArea(0, 15)] public string instructionData;
    }
    [Header("Player interaction instuctions")]
    [SerializeField] Instructions[] instructions;
    [Header("UI Elements")]
    [Header("Resources")]
    [SerializeField] public TextMeshProUGUI coinsTextField;
    [SerializeField] public TextMeshProUGUI logsTextMeshPro;
    [Header("Instructions text field")]
    [SerializeField] TextMeshProUGUI instructionsTextField;
    [Header("Text Background")]
    [SerializeField] Image textBackground;
    public static int logsInInventory = 0;
    public static int coinsInInventory = 0;
    Dictionary<string, Instructions> instructionText;
    //Dictionary object initialization & filling & background deactivation...
    void Init() {
        instructionText = new Dictionary<string, Instructions>();
        SetBackgroundState(false);
        for (int i = 0; i < instructions.Length; i++) {
            instructionText.Add(instructions[i].instructionName, instructions[i]);
        }
    }
    private void Start() { Init(); }
    // Update is called once per frame
    void Update() { UpdateUIData(); }
    //Updating ui resources elements...
    void UpdateUIData() {
        coinsTextField.text = $"Coins : {coinsInInventory}";
        logsTextMeshPro.text = $"Logs : {logsInInventory}";
    }
    //Method to activate/deactivate the text background image..
    public void SetBackgroundState(bool activeState) {
        textBackground.gameObject.SetActive(activeState);
    }
    //Display the value that corresponds to a key...  
    public void SetInstructions(string name) {
        instructionsTextField.text = instructionText[name].instructionData;
    }
}