using System;
using System.Text;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarTelemetryHandler _carTelemetryHandler;
    private ObjectTelemetryData _objectTelemetryData;
    private UDPRequests _udpRequests;
    private Thread _handler;

    private void Start()
    {
        InitializeUdpRequest();
        InitializeParameters();
    }

    private void InitializeParameters()
    {
        _objectTelemetryData = new ObjectTelemetryData();

        _carTelemetryHandler.SetObjectTelemetryData(_objectTelemetryData);
    }

    private void InitializeUdpRequest()
    {
        _udpRequests = new UDPRequests();
        _handler = new Thread(ObjectTelemetryDataHandler);
        _handler.Start();
    }

    private void OnDisable()
    {
        _handler.Abort();
    }

    private void ObjectTelemetryDataHandler()
    {
        while (true)
        {
            Thread.Sleep(100);
            if (_udpRequests == null || _objectTelemetryData == null)
            {
                continue;
            }

            var dataToSend = $"{_objectTelemetryData.Pitch}_" +
                             $"{_objectTelemetryData.Roll}_" +
                             $"{_objectTelemetryData.Yaw}_" +
                             $"{_objectTelemetryData.Surge}_" +
                             $"{_objectTelemetryData.Sway}_" +
                             $"{_objectTelemetryData.Heave}_" +
                             $"{_objectTelemetryData.Speed}";

            var bytesToSend = Encoding.ASCII.GetBytes(dataToSend);

            _udpRequests.Write(bytesToSend);
        }
    }
}