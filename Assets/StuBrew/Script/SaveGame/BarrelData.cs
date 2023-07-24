using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BarrelData
{
    public float bitterness = 0;
    public float sweetness = 0;
    public float aroma = 0;

    public float volume;
    public float currentFill;

    public Color color = Color.white;
    public float transparency = 0;     //0-1
    public Color foamColor = Color.white;

    public float[] position;

    public BarrelData(Transform barrelTransform, LiquidProperties liquidProperties, BarrelContainer barrelContainer)
    {
        position = new float[3];
        position[0] = barrelTransform.position.x;
        position[1] = barrelTransform.position.y;
        position[2] = barrelTransform.position.z;

        bitterness = liquidProperties.GetBitterness();
        sweetness = liquidProperties.GetSweetness();
        aroma = liquidProperties.GetAroma();

        volume = barrelContainer.GetVolume();
        currentFill = barrelContainer.GetCurrentStored();

        color = liquidProperties.GetColor();
        foamColor = liquidProperties.GetFoamColor();
        transparency = liquidProperties.GetTransparency();
    }

    public GameObject Load(GameObject respawnPrefab)
    {
        GameObject obj = Object.Instantiate(respawnPrefab, new Vector3(position[0], position[1], position[2]), Quaternion.Euler(0, 0, 0));
        if (obj.TryGetComponent(out LiquidProperties prop))
        {
            prop.SetBitterness(bitterness);
            prop.SetSweetness(sweetness);
            prop.SetAroma(aroma);

            prop.SetColor(color);
            prop.SetFoamColor(foamColor);
            prop.SetTransparency(transparency);
        }

        if (obj.TryGetComponent(out BarrelContainer container))
        {
            container.SetVolume(volume);
            container.SetCurrentStored(currentFill);
        }

        return obj;
    }
}
