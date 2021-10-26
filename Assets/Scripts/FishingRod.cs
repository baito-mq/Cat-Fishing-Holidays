using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public BoxCollider WaterChecker;
    public LayerMask WaterLayer;
    public ThirdPersonMovement PlayerMovement;
    public GameObject Camera;
    public GameObject RhythmGamePrefab;

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
         GameObject clone = Object.Instantiate(RhythmGamePrefab);
  clone.transform.position = new Vector3(1000, 0, 1000);
  Camera.transform.position= clone.transform.position- new Vector3(0,-540,350);
  Camera.transform.rotation= Quaternion.identity;
  RhythmManager Anky=clone.GetComponent<RhythmManager>();
  Anky.NoteSpeed= 400f;
  Anky.SpawnRandom();

    }

    public void StopFishing()
    {
        IsFishing = false;
        PlayerMovement.enabled = true;
    }
}
