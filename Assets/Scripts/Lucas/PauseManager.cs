using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public void Pause(bool can_pause)
    {
        if(can_pause)
            Time.timeScale = 0;
        if (!can_pause)
            Time.timeScale = 1;
    }
}
