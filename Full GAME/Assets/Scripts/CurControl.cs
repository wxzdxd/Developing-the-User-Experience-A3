using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public interface ICur
{

}

/// <summary>
/// �����������
/// </summary>
public static class CurControl
{
    public static bool MainLock = false;
    /// <summary>
    /// Ҫ���������˺�
    /// </summary>
    private static List<ICur> lockUsers = new List<ICur>();
    /// <summary>
    /// Ҫ�ͷ������˺�
    /// </summary>
    private static List<ICur> releaseUsers = new List<ICur>();
    
    static void CheckUsers()
    {
        for(int i = 0; i < lockUsers.Count; i++)
        {
            if (lockUsers[i] == null)
            {
                lockUsers.RemoveAt(i);
                i--;
            }
        }
        for(int i = 0; i < releaseUsers.Count; i++)
        {
            if (releaseUsers[i] == null)
            {
                releaseUsers.RemoveAt(i);
                i--;
            }
        }

    }
    /// <summary>
    /// �ͷ���꣬��Ҫ��������Ϊ��Ҫ������Ȳ�����
    /// </summary>
    public static void ReleaseCur(ICur account)
    {
        CheckUsers();
        if (lockUsers.Contains(account))
            lockUsers.Remove(account);
        if(!releaseUsers.Contains(account))
            releaseUsers.Add(account);
        UpdateCurState();
    }
    public static void LockCur(ICur account)
    {
        CheckUsers();
        if (!lockUsers.Contains(account))
            lockUsers.Add(account);
        if (releaseUsers.Contains(account))
            releaseUsers.Remove(account);
        UpdateCurState();
    }
    public static bool isLock = false;
    static bool Change = false;
    public static void Clear()
    {
        releaseUsers.Clear();
        lockUsers.Clear();
        UpdateCurState();
    }
    static void UpdateCurState()
    {
        Change = false;
        if (releaseUsers.Count > 0)
            ChangeLock(false);
        else if (lockUsers.Count > 0)
            ChangeLock(true);
        else
            ChangeLock(MainLock);
        if (Change)
            Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
    }
    static void ChangeLock(bool t)
    {
        if(isLock!=t)
        {
            Change = true;
            isLock = t;
        }
    }
}
