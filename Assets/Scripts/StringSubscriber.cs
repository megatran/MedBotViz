using ROS2;
using ROS2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StringSubscriber : MonoBehaviour
{
    INode node;
    ISubscription<std_msgs.msg.String> chatter_sub;
    public TMP_Text textObject;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            RCLdotnet.Init();
        }
        catch (UnsatisfiedLinkError e)
        {
            Debug.Log(e.ToString());
        }

        node = RCLdotnet.CreateNode("listenerHolo");
        Debug.Log("Node created");
        //chatter_sub = node.CreateSubscription<std_msgs.msg.String>(
        //    "chatter", msg => Debug.Log("I heard: [" + msg.Data + "]"), QosProfile.Profile.Default);
        chatter_sub = node.CreateSubscription<std_msgs.msg.String>(
            "chatter", msg => callback(msg), QosProfile.Profile.Default);

    }

    void callback(std_msgs.msg.String msg)
    {
        Debug.Log("Received: " + msg.Data);
        textObject.text = msg.Data;
    }

    // Update is called once per frame
    void Update()
    {
        RCLdotnet.SpinOnce(node, 0);
    }
}
