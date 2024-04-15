using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private void Update()
    {
        if (isDie)
            return;
        Vector3 p = ThirdPersonInput.singleton.transform.position;
        p.y = transform.position.y;
        if (Vector3.Distance(p, transform.position) < 1f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDie = true;
                Die();
            }
        }
    }
    bool isDie = false;
    void Die()
    {
        ThirdPersonInput.singleton.SetEnable(false);
        GetComponent<Animator>().SetTrigger("Die");
        CretidsPanel.singleton.Show();
        CurControl.Clear();
    }
}
