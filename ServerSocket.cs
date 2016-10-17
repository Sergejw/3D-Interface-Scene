using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ServerSocket : MonoBehaviour {

    public Text text;
    IPAddress ipAddress;
    Explorer explorer;

    // Use this for initialization
    void Start()
    {
        explorer = new Explorer();
        //explorer.writeFile();
        explorer.SaveScene();
        //explorer.GetScene("d");
        Debug.Log(explorer.GetAllSceneNames());
        
        //StartListening();
        Thread myThread = new Thread(new ThreadStart(StartListening));
        myThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Ip: " + ipAddress + "Port: 11111";
    }

    // Incoming data from the client.
    public string data = null;

    public void StartListening()
    {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024]; 

        // Establish the local endpoint for the socket.
        // Dns.GetHostName returns the name of the 
        // host running the application.
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        ipAddress = ipHostInfo.AddressList[1];

        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11111);

        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and 
        // listen for incoming connections.
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10); 

            // Start listening for connections.
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.
                Socket handler = listener.Accept();
                data = null;
                int bytesRec = 1;

                // An incoming connection needs to be processed.
                while (true)
                {
                    bytes = new byte[1024];
                    bytesRec = handler.Receive(bytes);
                    //data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Debug.Log(data + "bytesRec" + bytesRec);

                    if (data.Equals("get_all_scene_names"))
                    {
                        handler.Send(Encoding.ASCII.GetBytes(explorer.GetAllSceneNames() + "\n"));
                    }

                    if (data.IndexOf("*") != -1 ||  bytesRec == 0)
                    {
                        break;
                    }
                }



                // Show the data on the console.
                Console.WriteLine("Text received : {0}", data);

                // Echo the data back to the client.
                byte[] msg = Encoding.ASCII.GetBytes(data);

                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }
}
