using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Trig : MonoBehaviour
{
    public UnityEvent trig;
    public int idx = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThirdPersonController>())
        {
            trig?.Invoke();

            if (idx != 0)
                SceneManager.LoadScene(idx);
        }
    }
}
