using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class ChangeQuality : MonoBehaviour
{
    public static ChangeQuality instance;

    [SerializeField] TMP_Dropdown qualitySettingsDropdown; // дроп
    [SerializeField] Toggle postProcessToggle; // 
    [SerializeField] Toggle antiAliasingToggle; // 
    [SerializeField] UniversalAdditionalCameraData universalAdditionalCameraData;
    bool antiAliasingEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = 0;
        Application.targetFrameRate = 300;

        if(postProcessToggle)
        {
            postProcessToggle.onValueChanged.AddListener(delegate 
            {
                if (universalAdditionalCameraData)
                {
                    universalAdditionalCameraData.renderPostProcessing = postProcessToggle.isOn;

                    if (postProcessToggle.isOn && antiAliasingEnabled)
                        antiAliasingToggle.SetIsOnWithoutNotify(true);

                    if(!postProcessToggle.isOn)
                        antiAliasingToggle.SetIsOnWithoutNotify(false);
                }
            });
        }

        if (antiAliasingToggle)
        {
            antiAliasingToggle.onValueChanged.AddListener(delegate 
            {
                if (universalAdditionalCameraData)
                {
                    universalAdditionalCameraData.antialiasing =
                    antiAliasingToggle.isOn ?
                    AntialiasingMode.FastApproximateAntialiasing :
                    AntialiasingMode.None;

                    antiAliasingEnabled = antiAliasingToggle.isOn;

                    if (antiAliasingToggle.isOn && !postProcessToggle.isOn)
                        postProcessToggle.isOn = true;
                }
            });
        }

        if (qualitySettingsDropdown) // если есть дроп
        {
            qualitySettingsDropdown.ClearOptions(); // очищаем опции дропа
            qualitySettingsDropdown.AddOptions(QualitySettings.names.ToList()); // добавляем имена качеств в опции дропа
            qualitySettingsDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel()); // устанавливаем текущий в дропе
            qualitySettingsDropdown.onValueChanged.AddListener(delegate 
            { // подписываемся на изменение значения дропа
                QualitySettings.SetQualityLevel(qualitySettingsDropdown.value, true); // меняем качество на текущее значение в дропе
                Debug.Log($"Quality level: {QualitySettings.names[QualitySettings.GetQualityLevel()]}"); // выводим в лог
            });
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnDestroy()
    {
        if(postProcessToggle)
            postProcessToggle.onValueChanged.RemoveAllListeners();
        if (antiAliasingToggle)
            antiAliasingToggle.onValueChanged.RemoveAllListeners();
        if (qualitySettingsDropdown)
            qualitySettingsDropdown.onValueChanged.RemoveAllListeners();
    }

    public void UPGraphcis()
    {
        var qualityLevel = QualitySettings.GetQualityLevel();
        //if (ql < 2)
        //    ql += 1;
        //else
        //    ql = 0;
        qualityLevel = qualityLevel < 2 ? +1 : 0;

        QualitySettings.SetQualityLevel(qualityLevel, true);
    }
}
