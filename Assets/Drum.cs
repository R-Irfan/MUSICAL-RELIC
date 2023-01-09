using Niantic.ARDK.Utilities.Input.Legacy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    
    AudioSource sound;
    

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Hit test against the placed objects, and remove if a placed object was tapped.
            // Else, place an object at the tap location.
            var worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(worldRay, out hit, 1000f))
            {
                Debug.Log(hit.transform.name);
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) 
        {
            Debug.Log("Hand Touched");
            PlaySound();

        }
    }

    public void PlaySound() 
    {
        sound.Play();
    }
}
