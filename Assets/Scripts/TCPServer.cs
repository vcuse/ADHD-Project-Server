using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class TCPServer : MonoBehaviour
{
    private TcpListener server;
    private bool isRunning = false;
    [SerializeField] private Timer timerScript;

    void Start()
    {
        StartServer();
    }

    void OnDestroy()
    {
        StopServer();
    }

    void StartServer()
    {
        try
        {
            // Initialize the server socket
            IPAddress ipAddress = IPAddress.Any; ; // Listen on any available IP address
            int port = 32401; // Choose your desired port number
            server = new TcpListener(ipAddress, port);
            server.Start();
            isRunning = true;

            // Start a new thread to handle incoming connections
            Thread thread = new Thread(ListenForClients);
            thread.Start();
            Debug.Log("Server started.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error starting server: " + e.Message);
        }
    }

    void StopServer()
    {
        if (isRunning)
        {
            isRunning = false;
            server.Stop();
            Debug.Log("Server stopped.");
        }
    }

    void ListenForClients()
    {
        try
        {
            while (isRunning)
            {
                // Accept incoming client connections
                TcpClient client = server.AcceptTcpClient();
                Debug.Log("Client connected.");

                // Start a new thread to handle client communication
                Thread clientThread = new Thread(HandleClientCommunication);
                clientThread.Start(client);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error accepting client connection: " + e.Message);
        }
    }

    void HandleClientCommunication(object clientObject)
    {
        TcpClient client = (TcpClient)clientObject;
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        try
        {
            // Read data from the client
            while (true)
            {
                float mouseTime = timerScript.GetMouseTime();
                float kbTime = timerScript.GetKBTime();

                // Construct a message containing the timer values
                string timerData = "MouseTime:" + mouseTime + " KBTime:" + kbTime;

                // Send the timer values back to the client
                byte[] responseDataBytes = Encoding.ASCII.GetBytes(timerData);
                stream.Write(responseDataBytes, 0, responseDataBytes.Length);
                Debug.Log("Sent timer data to client: " + timerData);
                // Optionally, you can send data back to the client
                // string responseData = "Message received!";
                // byte[] responseDataBytes = Encoding.ASCII.GetBytes(responseData);
                // stream.Write(responseDataBytes, 0, responseDataBytes.Length);
            }

            // Close the connection
            client.Close();
            Debug.Log("Client disconnected.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error handling client communication: " + e.Message);
        }
    }
}
