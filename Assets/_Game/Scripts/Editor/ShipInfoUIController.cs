using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

[CustomEditor(typeof(ShipInfo))]
public class ShipInfoUIController : Editor
{
    [SerializeField] private VisualTreeAsset ShipInfoUI;

    [SerializeField] public FloatVariable ThrottleValue;
    [SerializeField] public FloatVariable RotationValue;
    public override VisualElement CreateInspectorGUI()
    {
        ThrottleValue = AssetDatabase.LoadAssetAtPath<FloatVariable>("Assets/_Game/Components/Ship/ThrottlePower.asset");
        if (ThrottleValue == null)
        {
            Debug.Log("Failed to load Throttle Value scriptable object of type Float Variable check if the path is correct");
        }

        RotationValue = AssetDatabase.LoadAssetAtPath<FloatVariable>("Assets/_Game/Components/Ship/RotationPower.asset");
        if (RotationValue == null)
        {
            Debug.Log("Failed to load Rotation Value scriptable object of type Float Variable check if the path is correct");
        }


        VisualElement myInspector = new VisualElement();

        ShipInfoUI.CloneTree(myInspector);

        Slider SliderThrottle = myInspector.Q<Slider>("SliderThrottle");
        Slider SliderRotation = myInspector.Q<Slider>("SliderRotation");

        SliderThrottle.value = ThrottleValue.Value;
        SliderRotation.value = RotationValue.Value;
        
        SliderThrottle.RegisterCallback<ChangeEvent<float>>(evt =>
        {
            ThrottleValue.SetValue(evt.newValue);

            Debug.Log(ThrottleValue.Value);
        });

        SliderRotation.RegisterCallback<ChangeEvent<float>>(evt =>
        {
            RotationValue.SetValue(evt.newValue);

            Debug.Log(RotationValue.Value);
        });

        return myInspector;
    }
}
