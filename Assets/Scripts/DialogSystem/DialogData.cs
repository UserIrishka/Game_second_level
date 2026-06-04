using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Scriptable Objects/DialogData")]
public class DialogData : ScriptableObject
{
    public List <DialogElements> DialogElements;
}

[Serializable]
public class DialogElements
{
    public Sprite PortretSprite;
    [TextArea] public string DialogText;
}
