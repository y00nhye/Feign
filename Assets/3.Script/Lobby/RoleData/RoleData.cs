using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "Scriptable Object/Role Data")]
public class RoleData : ScriptableObject
{
    public string roleName;
    public Sprite roleImg;
    public string roleOrder;

    public int SerialNumC;
    public int SerialNumI;
    public int SerialNumN;
}
