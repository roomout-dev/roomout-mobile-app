using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeasureChest : MonoBehaviour
{
    [SerializeField] private Animator chestAnimationController;
    public GameObject endMessageObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                chestAnimationController.SetBool("openChest", true);
                endMessageObject.SetActive(true);
            }
        }
    }
}
