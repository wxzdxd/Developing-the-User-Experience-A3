using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音效的配置数据
/// </summary>
[CreateAssetMenu()]
public class EffectClipSO : ScriptableObject
{
    public EffectType type;
    public AudioClip clip;
}
