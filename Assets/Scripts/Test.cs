using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void TestA()
    {
        GameManager.INSTANCE.Call("blackjack");
    }
}
