using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = true;  // 显示鼠标指针
        Cursor.lockState = CursorLockMode.None; // 解除鼠标指针的锁定状态
    }

    // Update is called once per frame

    public void Game0()
    {
        SceneManager.LoadScene(0);//跳转到场景1
    }
    public void Game1()
    {
        SceneManager.LoadScene(1);//跳转到场景1
    }

    public void Tx()
    {
        Application.Quit();
    }
    public void Qc()
    {
        PlayerPrefs.DeleteAll();
    }
}
