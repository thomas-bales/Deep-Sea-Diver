using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishImageAI : MonoBehaviour
{
    public Transform parent;
    void Update()
    {
        if (parent)
            transform.position = parent.position;
        else
            Destroy(gameObject);
    }
}
