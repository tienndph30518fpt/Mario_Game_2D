using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLever : MonoBehaviour
{
    public string manChoi;

    public void chuyenManChoi()
    {
        SceneManager.LoadScene(manChoi);
        Time.timeScale = 1;
    }
}
