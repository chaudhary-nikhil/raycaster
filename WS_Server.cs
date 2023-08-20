using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System;

public class WS_Server : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("STARTING");
        string ip = "172.22.214.222";
        int port = 8000;
        var server = new TcpListener(IPAddress.Parse(ip), port);

        server.Start();
        Debug.Log("Server has started on " + ip + ":" + port + ", Waiting for a connection…");

        TcpClient client = server.AcceptTcpClient();
        Debug.Log("A client connected.");

        NetworkStream stream = client.GetStream();
    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("Update Console Write");
    }
}
