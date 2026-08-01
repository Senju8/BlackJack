using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void TestA()
    {
        GameManager.INSTANCE.Invoke(this.gameObject);
    }
}
