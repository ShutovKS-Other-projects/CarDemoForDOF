using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPRequests
{
    public UDPRequests(string serverIp = "127.0.0.1", int serverPort = 4123)
    {
        _udpClient = new UdpClient(serverIp, serverPort);
    }

    private UdpClient _udpClient;
    private ObjectTelemetryData _objectTelemetryData;

    public void Write(byte[] bytes)
    {
#if UNITY_EDITOR
        // Debug.Log(Encoding.ASCII.GetString(bytes));
#endif
        _udpClient.Send(bytes, bytes.Length);
    }
}