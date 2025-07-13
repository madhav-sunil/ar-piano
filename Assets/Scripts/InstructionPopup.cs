using UnityEngine;

public class InstructionPopup : MonoBehaviour
{
    public Canvas instructionCanvas;

    public void HidePopup()
    {
        if (instructionCanvas != null)
            instructionCanvas.enabled = false;
    }
}

