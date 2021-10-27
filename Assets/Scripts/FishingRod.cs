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
private GameObject clone;
private Vector3 Cam1= new Vector3(11,6,-14);
    private bool IsFishing = false;

    public void Update()
    {
        if (Input.GetButtonDown("Fish"))// press E 
        {
            if (!IsFishing)// if it is not fishing
            {//check if water is nearby 
                bool value = Physics.CheckBox(WaterChecker.transform.position, WaterChecker.bounds.extents, WaterChecker.transform.rotation, WaterLayer);
                if (value)//true
                {
                    StartFishing();
                }
            } else//false
            {
                StopFishing();
            }
        }
		if(Input.GetButtonDown("End"))
	{
		if(IsFishing==true){
		
		Camera.transform.position= Cam1;//11,6,-14, 
		Camera.transform.rotation= Quaternion.Euler(17,0,0);
		StopFishing();
		Destroy(clone);
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
Destroy(clone,15);

    }
	


	
    public void StopFishing()
    {
        IsFishing = false;
        PlayerMovement.enabled = true;
		
		
    }
}
