using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector3 EnterValue = 1.2f * Vector3.one;
    public Vector3 normalValue = Vector3.one;

    public UnityEvent onClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioMgr.singleton.PlaySoundEffect(EffectType.BtnClick);
        eventData.Use();
        onClick?.Invoke();
    }
    public void SetBG(float value) { AudioMgr.singleton.SetBackgroundVolume(value); }
    public void SetEF(float value) { AudioMgr.singleton.SetSoundEffectVolume (value); }
    public void Quit() { Application.Quit(); }
    public void LoadS(int idx) { SceneManager.LoadScene(idx); }
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.Use();
        transform.localScale = EnterValue;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.Use();
        transform.localScale = normalValue;
    }
}