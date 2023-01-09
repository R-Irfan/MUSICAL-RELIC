using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Niantic.ARDK.AR.Awareness;
using ARDK.Extensions;
using System;

public class HandTracking : MonoBehaviour
{
    [SerializeField]
    ARHandTrackingManager _handTrackingManager;

    IReadOnlyList<Detection> _detections;

    bool grabbed = false;
    float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        _handTrackingManager.HandTrackingUpdated += OnHandTrackingUpdated;
    }

    void OnHandTrackingUpdated(HumanTrackingArgs args)
    {
        //var data = args.TrackingData;
        /* for (var i = 0; i < data.AlignedDetections.Count; i++)
         {
             var item = data.AlignedDetections[i];
             Debug.Log(item.X + " : " + item.Y + " : " + item.Width + " : "  + item.Height);
         }*/

        _detections = args.TrackingData?.AlignedDetections;



    }

    // Update is called once per frame
    void Update()
    {
        if (_detections != null)
        {
           
            var detection = _detections[0];

            Vector3 detectionSize = new Vector3(detection.Rect.width, detection.Rect.height, 0);
            var depthEstimation = 0.2f + Mathf.Abs(1 - detectionSize.magnitude);
            depthEstimation = depthEstimation - 0.4f;
            //Debug.Log("Depth  : " + depthEstimation);
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(detection.Rect.xMin, 1 - detection.Rect.yMin, depthEstimation));

            transform.position = Vector3.Lerp(transform.position, pos, .5f);
            //Debug.Log("Hand Position : " + transform.position);

        }
       
       


        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Debug.Log("Mouse Down");
            grabbed = true;
        }


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grabbed = false;
        }

       


        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;


    }


}
