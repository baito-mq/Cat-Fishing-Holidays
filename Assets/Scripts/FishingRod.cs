using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public BoxCollider WaterChecker;
    public LayerMask WaterLayer;
    public ThirdPersonMovement PlayerMovement;

    private bool IsFishing = false;

    public void Update()
    {
        if (Input.GetButtonDown("Fish"))
        {
            if (!IsFishing)
            {
                bool value = Physics.CheckBox(WaterChecker.transform.position, WaterChecker.bounds.extents, WaterChecker.transform.rotation, WaterLayer);
                if (value)
                {
                    StartFishing();
                }
            } else
            {
                StopFishing();
            }
        }
    }

    public void StartFishing()
    {
        IsFishing = true;
        PlayerMovement.enabled = false;
    }

    public void StopFishing()
    {
        IsFishing = false;
        PlayerMovement.enabled = true;
    }
}
