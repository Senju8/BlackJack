using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void TestA(int index)
    {
        GameManager.INSTANCE.Invoke(this.gameObject,index);
    }
}
