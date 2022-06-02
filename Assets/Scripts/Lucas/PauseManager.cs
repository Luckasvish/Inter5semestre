using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    GameObject PauseUI;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(true);
        }
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.P))
        {
            Bank.instance.ChangeMoneyAmount(20, true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse4))
        {
            SceneManage.GoToNextScene();
        }
#endif
    }
    public void Pause(bool can_pause)
    {
        if (can_pause)
            Time.timeScale = 0;
        if (!can_pause)
            Time.timeScale = 1;
        PauseUI.SetActive(can_pause);
    }
}
