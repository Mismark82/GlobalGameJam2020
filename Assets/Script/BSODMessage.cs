using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BSODMessageDB", menuName = "BSODManager", order = 1)]
public class BSODMessage : ScriptableObject
{
    [TextArea]
    public List<string> messaggio;
}
