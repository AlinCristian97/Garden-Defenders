using UnityEngine;
using UnityEngine.UI;

namespace General.Options
{
    public class SetQuality : MonoBehaviour
    {
        public void SetVideoQuality(int dropdownValue)
        {
            QualitySettings.SetQualityLevel(dropdownValue);
            PlayerPrefs.SetInt("VideoQuality", dropdownValue);
        }
    }
}