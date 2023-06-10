using UnityEngine;
using UnityEngine.UI;

public class WheelColliderModifier : MonoBehaviour
{
    public Slider slider;
    public WheelCollider[] wheelColliders;
    public float minValue;
    public float maxValue;

    private void Start()
    {
        // Set the initial value of the slider
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.onValueChanged.AddListener(UpdateWheelColliders);
    }

    private void UpdateWheelColliders(float value)
    {
        foreach (WheelCollider wheelCollider in wheelColliders)
        {
            // Modify the desired properties of the Wheel Collider based on the slider value
            wheelCollider.suspensionDistance = value;
            // Add more modifications as needed

            // Apply the changes to the Wheel Collider
            wheelCollider.ConfigureVehicleSubsteps(5, 12, 15);
        }
    }
}
