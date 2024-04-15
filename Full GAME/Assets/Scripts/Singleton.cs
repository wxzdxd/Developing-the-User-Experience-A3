
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T singleton
    {
        get
        {
            return instance;
        }
    }
    public virtual void Awake()
    {
        if (!instance)
            instance = this as T;
        if (instance != this)
            Destroy(gameObject);
    }
}
public class Panel<T> : MonoSingleton<Panel<T>> where T:Panel<T>
{
    public Transform Entity;
    public override void Awake() { base.Awake(); if (!Entity) Entity = transform.GetChild(0); }
    public virtual void Show()
    {
        Entity.gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        Entity.gameObject.SetActive(false);
    }
}