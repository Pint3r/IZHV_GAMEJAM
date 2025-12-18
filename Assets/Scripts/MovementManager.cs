using UnityEngine;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    [Header("Scripts")]
    public MonoBehaviour scriptWithSpasm;
    public MonoBehaviour scriptSimple;

    [Header("UI to Hide")]
    public GameObject spasmSliderObj;

    void Awake()
    {
        bool hasCure = PlayerPrefs.GetInt("HasCure", 0) == 1;

        if (hasCure)
        {
            scriptWithSpasm.enabled = false;

            scriptSimple.enabled = true;

            spasmSliderObj.SetActive(false);
        }
        else
        {
            scriptWithSpasm.enabled = true;

            scriptSimple.enabled = false;
            spasmSliderObj.SetActive(true);

        }
    }
}