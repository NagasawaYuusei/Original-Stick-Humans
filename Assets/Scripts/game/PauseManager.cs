using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool m_pauseFlg = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            PauseResume();
        }
    }
    void PauseResume()
    {
        m_pauseFlg = !m_pauseFlg;

        var objects = FindObjectsOfType<GameObject>();

        foreach (var o in objects)
        {
            IPause i = o.GetComponent<IPause>();

            if (m_pauseFlg)
            {
                i?.Pause();
            }
            else
            {
                i?.Resume(); 
            }
        }
    }
}

