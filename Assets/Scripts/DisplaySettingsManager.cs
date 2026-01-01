using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DisplaySettingsManager : MonoBehaviour
{
    public Slider fpsSlider;
    public Slider renderScaleSlider;
    public Slider qualityLevelSlider;

    public Toggle pptoggle;

    public TMP_Text qltext;
    public TMP_Text fpstext;

    public TMP_Text rstext;

    public Saves sv;
    public MainLogic logic;


    void Awake()
    {
        sv = FindObjectOfType<Saves>();
        logic = FindObjectOfType<MainLogic>();
    }

    public void onQualityLevelSliderChange()
    {
        switch (qualityLevelSlider.value)
        {
            case 0:
                qltext.text = "Low";
            break;
            case 1:
                qltext.text = "Medium";
            break;
            case 2:
                qltext.text = "High";
            break;
        }
    }

    public void onFPSSliderChange()
    {
        if(fpsSlider.value == -1)
        {
            fpstext.text = "Unlimited";
        } else
        {
            fpstext.text = fpsSlider.value.ToString();
        }
        
    }
    public void onRenderScaleSliderChange()
    {
        float val = renderScaleSlider.value*0.15f+0.7f;
        rstext.text = val.ToString();
    }
    public void SaveChanges()
    {
        int val;
        if (pptoggle.isOn) val = 1;
        else
        val = 0;
        sv.saveGraphicsSettings((int)fpsSlider.value, (int)renderScaleSlider.value, (int)qualityLevelSlider.value, val);
        logic.applyGlobalGSettings();

    }
    public void loadSaves()
    {
        fpsSlider.value = sv.getFPS();
        renderScaleSlider.value = sv.getRScale();
        qualityLevelSlider.value = sv.getQlvl();
        int flag = sv.getPPS();
        if(flag == 1)
        {
            pptoggle.isOn = true;
        } else
        {
            pptoggle.isOn = false;
        }

    }
    
}
