using UnityEngine;
using UnityEngine.Serialization;

public class CarTelemetryHandler : MonoBehaviour
{
    [SerializeField] private Transform _vehicleTransform;
    [SerializeField] private Rigidbody _rigidbody;
    private ObjectTelemetryData _telemetryDataData;

    public void SetObjectTelemetryData(ObjectTelemetryData objectTelemetryData)
    {
        _telemetryDataData = objectTelemetryData;
    }
    
    private void Update()
    {
        if (_telemetryDataData == null)
        {
            return;
        }
        
        var rotation = _vehicleTransform.rotation;
        _telemetryDataData.Pitch = rotation.eulerAngles.x;
        _telemetryDataData.Roll = rotation.eulerAngles.z;
        _telemetryDataData.Yaw = rotation.eulerAngles.y;

        var position = _vehicleTransform.position;
        _telemetryDataData.Surge = position.z;
        _telemetryDataData.Sway = position.x;
        _telemetryDataData.Heave = position.y;

        _telemetryDataData.Speed = _rigidbody.velocity.magnitude;
    }
}