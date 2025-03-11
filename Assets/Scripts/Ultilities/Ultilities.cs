using System;
using System.Collections;
using UnityEngine;

public class Ultilities : MonoBehaviour
{
    public static IEnumerator DoActionAfterSeconds(Action CallBack, float secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);

        CallBack?.Invoke();
    }
}
