using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardButton : MonoBehaviour
{
        public GameObject pdfBoard; // Reference to PDF board GameObject
        public GameObject whiteboard; // Reference to whiteboard GameObject
    
        public void PressedButton()
        {
            if (whiteboard.activeSelf)
            {
                whiteboard.SetActive(false);
                pdfBoard.SetActive(true);
            }
            else
            {
                whiteboard.SetActive(true);
                pdfBoard.SetActive(false);
            }
        }
}
