using Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

[CustomEditor(typeof(ShipInfo))]
public class ShipInfoUIController : Editor
{
    [SerializeField] private VisualTreeAsset ShipInfoUI;

    [SerializeField] public GameObject ShipRefference;
    [SerializeField] public FloatVariable ThrottleValue;
    [SerializeField] public FloatVariable RotationValue;

    public override VisualElement CreateInspectorGUI()
    {
        if (ShipRefference == null)
        {
            ISearchList Results = SearchService.Request("p:t:Engine", SearchFlags.Synchronous);

            List<string> ShipPaths = new List<string>();
            string ShipPath = "";

            SearchService.Request("t:Engine", (SearchContext context, IList<SearchItem> items) =>
            {
                foreach (var item in items)
                {
                    string strin = item.data.ToString();
                    string strin2 = "";
                    string strin3 = "";
                    ShipPath = "";
                    bool foundpath = false;
                    foreach (char c in strin)
                    {
                        if (foundpath)
                        {
                            strin2 += c;
                            if (strin2.Contains(".prefab"))
                            {
                                for (int i = 0; i < strin2.Length - 7; i++)
                                {
                                    ShipPath += strin2[i];
                                }
                                ShipPaths.Add(ShipPath);
                                Debug.Log(ShipPath);
                                break;
                            }
                        }
                        else
                        {
                            strin3 += c;
                            if (strin3.Contains("/Resources/"))
                            {
                                foundpath = true;
                            }
                        }
                    }
                    //for some reasons some operations like .Replace do not work in this scope
                }

            }, SearchFlags.Debug);

            ShipPath = ShipPaths[0];
            if (ShipPath == "")
            {
                Debug.Log("No Asset Found cheeck if there is a prefab with an engine component on it");
                return null;
            }
            ShipRefference = Instantiate(Resources.Load(ShipPath, typeof(GameObject))) as GameObject;
            if (ShipRefference == null)
            {
                Debug.LogWarning("Failed to load Ship Refference cheeck if there is a prefab with an engine component on it");
                return null;
            }
            else
            {
                Engine engine = ShipRefference.GetComponent<Engine>();

                ThrottleValue = engine._throttlePower;
                RotationValue = engine._rotationPower;
            }

            DestroyImmediate(ShipRefference);
        }

        if (ShipRefference == null)
        {
            Debug.LogWarning("Failed to load Ship Refference chcek if there is a prefab with a Engine script in the resource folder");
            return null;
        }
        if (ThrottleValue == null)
        {
            Debug.LogWarning("there is no throttle in the ship refference");
            return null;
        }
        if (RotationValue == null)
        {
            Debug.LogWarning("there is no rotation in the ship refference");
            return null;
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
