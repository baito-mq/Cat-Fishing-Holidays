using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FishingRod : MonoBehaviour
{
    public BoxCollider WaterChecker;
    public LayerMask WaterLayer;
    public ThirdPersonMovement PlayerMovement;
    public GameObject Camera;
    public GameObject RhythmGamePrefab;
public GameObject clone;
public GameObject Anky;
private Vector3 Cam1= new Vector3(11,6,-14);
public RhythmManager score; 
    private bool IsFishing = false;
private float delay = 1;




    public void Update()
    {
		
		
				
		 if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home)) 
        { 
              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }  
	
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
		DestroyImmediate(clone);
	}
    }
	}
    public void LateUpdate(){

	}
	
	public void StartFishing()
    {
        IsFishing = true;
        PlayerMovement.enabled = false;
		   clone = Instantiate(RhythmGamePrefab) as GameObject;  
        // GameObject clone = Object.Instantiate(RhythmGamePrefab);
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