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
        _telemetryDataData.Pitch = rotation.eulerAngles.x > 180
            ? rotation.eulerAngles.x - 360
            : rotation.eulerAngles.x;
        _telemetryDataData.Roll = rotation.eulerAngles.z > 180
            ? rotation.eulerAngles.z - 360
            : rotation.eulerAngles.z;
        _telemetryDataData.Yaw = rotation.eulerAngles.y > 180
            ? rotation.eulerAngles.y - 360
            : rotation.eulerAngles.y;

        var velocity = _rigidbody.velocity;
        _telemetryDataData.Surge = velocity.z;
        _telemetryDataData.Sway = velocity.x;
        _telemetryDataData.Heave = velocity.y;

        _telemetryDataData.Speed = velocity.magnitude;
    }
}