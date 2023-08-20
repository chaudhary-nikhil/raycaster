using WebSocketSharp;
using UnityEngine;
using System;

public class WS_Client 
{
    WebSocket ws;
    int counter = 0;
    public float xCoord;
    public float yCoord;

    public WS_Client()
    {

        ws = new WebSocket("ws://192.168.86.144:8080");
        ws.Connect();
        //var counter = 1;

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("We are connected");

        };

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("message received");
            string input = e.Data;
            var numbers = input.Split(" ");

            try
            {
                xCoord = float.Parse(numbers[0]);
                Console.WriteLine(xCoord);
            }
            catch(FormatException d) {
                Console.WriteLine(d.Message);
            }
            try
            {
                yCoord = float.Parse(numbers[1]);
                Console.WriteLine(yCoord);
            }
            catch (FormatException d)
            {
                Console.WriteLine(d.Message);
            }
            Debug.Log("Message received from, Data X : " + xCoord + ", " + yCoord + " end");
        };
    }

    
    private void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Space))
        {
            counter++;

            ws.Send("Unity Message");
            Debug.Log("SPACe PRESSED");
        }


    }
    
    
}
