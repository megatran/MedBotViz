using ROS2;
using ROS2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RplidarSubscriber : MonoBehaviour
{

    INode node;
    ISubscription<sensor_msgs.msg.LaserScan> lidar_sub;
    GameObject[] lidarScans;
    public float heightOffset = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        lidarScans = new GameObject[720];
        // dynamically create a point cloud array
        for (int i = 0; i < 720; ++i)
        {
            lidarScans[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            lidarScans[i].transform.parent = this.gameObject.transform;
            lidarScans[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            lidarScans[i].transform.localPosition = new Vector3(0.2f, -0.2f, 0.7f);
            lidarScans[i].GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }

        Debug.Log("starting rplidar subscriber node");
        try
        {
            RCLdotnet.Init();
        }
        catch (UnsatisfiedLinkError e)
        {
            UnityEngine.Debug.Log(e.Message);
        }


        node = RCLdotnet.CreateNode("rplidar_listener");
        lidar_sub = node.CreateSubscription<sensor_msgs.msg.LaserScan>(
            "/scan_processed", msg => OnLidarScanReceived(msg), QosProfile.Profile.SensorData); 

    }

    void OnLidarScanReceived(sensor_msgs.msg.LaserScan msg)
    {
        List<float> ranges = msg.Ranges;

        float angle_increment = msg.Angle_increment;
        float angle_min = msg.Angle_min;
        float angle_max = msg.Angle_max;

        float angle = angle_min;

        for (int i = 0; i < ranges.Count; i++)
        {
            if (ranges[i] < 2000f)
            {
                lidarScans[i].transform.localPosition = new Vector3((float)Math.Sin(angle) * ranges[i], heightOffset, (float)Math.Cos(angle) * ranges[i]);
            }
            
            angle +=  angle_increment;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (node != null)
        {
            RCLdotnet.SpinOnce(node, 0);
        }
    }
}
