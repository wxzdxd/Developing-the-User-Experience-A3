using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CretidsPanel : Panel<CretidsPanel>
{
    public override void Show()
    {
        base.Show();
        StartCoroutine(s());
    }
    public void Qui()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
    public void Main()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator s()
    {
        ov.SetActive(false );
        float value = 0;
        bool isclick = false;
        while (value < 1)
        {
            value += Time.deltaTime / 10f;
            if (!isclick && value > 0.5f) isclick = true;
            Entity.localPosition = Vector3.Lerp(Vector3.down * Screen.height*1.5f, Vector3.up * Screen.height/2, value);

            yield return null;
            if (!isclick && Input.GetMouseButtonDown(0))
            {
                value = 0.5f;
            }
            else if (isclick && Input.GetMouseButtonDown(0))
                break;
        }
        Entity.localPosition = Vector3.up*Screen.height/2;

        yield return new WaitForSeconds(2);
        ov.SetActive(true);
    }
    public GameObject ov;
}