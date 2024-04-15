using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveGame : MonoBehaviour
{
    public void Leave()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_WEBGL)
                    Application.OpenURL("https://domenixius.itch.io/flickering-hope");
        #endif
    }
}
