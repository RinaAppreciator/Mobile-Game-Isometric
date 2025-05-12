using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour
{


    public float speed;

    public Renderer bgRenderer;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, -speed * Time.deltaTime);
    }
}
