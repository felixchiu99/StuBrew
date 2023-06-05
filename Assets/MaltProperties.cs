using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaltProperties : MonoBehaviour
{
    [SerializeField] Color maltLiquidColor = new Vector4(0.6745f, 0.3764f, 0.0156f);
    [SerializeField] float sweetness = 0f;
    [SerializeField] float bitterness = 0f;

    [SerializeField] ParticleSystem particleSystem;
    ParticleSystemRenderer rend;

    private Material maltMat;
    void Start()
    {
        if (!rend && particleSystem)
        {
            rend = particleSystem.GetComponent<ParticleSystemRenderer>();
        }
        if (rend)
        {
            maltMat = rend.material;
            maltMat.color = maltLiquidColor;
        }
            
    }

    public Color GetColor()
    {
        return maltLiquidColor;
    }

    public float GetSweetness()
    {
        return sweetness;
    }
    public float GetBitterness()
    {
        return bitterness;
    }

    public void Blend(MaltProperties other, float blendAmount)
    {
        sweetness += other.GetSweetness() * blendAmount;
        bitterness += other.GetBitterness() * blendAmount;
        maltLiquidColor = Color.Lerp( maltLiquidColor, other.GetColor(), blendAmount * 0.8f);
    }
}
