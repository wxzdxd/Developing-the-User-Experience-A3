using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionPanel : Panel<optionPanel>
{
    bool showing = false;
    public override void Awake()
    {
        base.Awake();

        if (instance == this)
            DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (showing)
                    Hide();
                else Show();
            }
        }
        else
        {
            if (showing)
            {
                Hide();
                CurControl.Clear();
            }
        }
    }
    public override void Show()
    {
        showing = true;
        CurControl.Clear();

        ThirdPersonInput.singleton.SetEnable(false);
        base.Show();
    }
    public override void Hide()
    {
        showing = false;
        ThirdPersonInput.singleton.SetEnable(true);
        base.Hide();
    }
}