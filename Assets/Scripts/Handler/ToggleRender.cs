using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRender : MonoBehaviour
{
    public void ToggleVisibility()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();

        if (rend.enabled) {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
        }
    }
}
