using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class LiquidContainerSetting : MonoBehaviour
{
    [Header("Liquid Setting")]
    [SerializeField]
    float maxWobble = 0.03f;

    //[SerializeField]
    float fillAmount = 0.5f;

    [SerializeField]
    float foamAmount = 0.1f;
    [SerializeField]
    float foamSmoothness = 0.2f;

    [SerializeField]
    float thickness = 1f;

    [SerializeField]
    bool isStationary = false;

    [SerializeField] bool overrideFill = false;

    [SerializeField] float volume;
    [SerializeField] float currentLiquidStored = 0f;

    [Space(10)]
    [Header("Operation")]
    [SerializeField]
    Renderer rend;
    [SerializeField]
    Rigidbody rb;

    [Space(10)]
    
    [Header("Pour Setting")]
    [HideIf("isStationary")]
    [SerializeField]    float rimRadius = 0.09f;
    [HideIf("isStationary")]
    [SerializeField]    float containerHeight;
    [HideIf("isStationary")]
    [SerializeField]    float pourMinTilt = 1f;
    [HideIf("isStationary")]
    [SerializeField]    ParticleContainer container;

    [HideIf("isStationary")]
    [SerializeField]    private Transform pour;
    [HideIf("isStationary")]
    [SerializeField]    private Transform liq;
    [HideIf("isStationary")]
    public bool isPour = false;

    [SerializeField]
    private AnimationCurve shapeFillCompensate;

    private Vector3 lastVelocity;

    private float wobbleX = 0;
    private float wobbleZ = 0;

    private Vector3 liqPos;

    void Start()
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        if (TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rb = rigidbody;
            lastVelocity = rb.velocity;
        }
        if(!rb){
            isStationary = true;
        }
        SetFill();
        SetRenderWobble();
        containerHeight = transform.localScale.y;
        if(volume == 0)
            volume = Mathf.PI * transform.localScale.x / 2 * transform.localScale.z / 2 * containerHeight;
    }

    void Update()
    {
        SetFill();
        if (!isStationary)
        {
            SetPour();
            CheckPour();
            PourParticle();
        }
    }

    private void FixedUpdate()
    {
        if (!isStationary)
        {
            Vector3 vel = rb.velocity;
            Vector3 acc = (vel - lastVelocity) / Time.fixedDeltaTime;
            float dif = Mathf.Abs((vel - lastVelocity).magnitude) / Time.fixedDeltaTime;
            rend.material.SetFloat("_Amplitude", dif * 0.001f);
            SetWobble(acc.x, acc.z);
            SetRenderWobble();
            lastVelocity = vel;
        }
    }

    void SetFill()
    {
        if (container && !isStationary)
        {
            fillAmount = container.GetFillLevel();
        }
        else if(!overrideFill)
        {
            fillAmount = volume == 0? 0 :currentLiquidStored / (float)volume;
        }
        
        float tiltAngle = Vector3.Angle(transform.up, Vector3.up);
        float compansate = EvaluateCurve(shapeFillCompensate, fillAmount);
        float fill = Mathf.Lerp(fillAmount, compansate, Mathf.Clamp(tiltAngle / 90, 0, 1));
        rend.material.SetFloat("_Fill", fill);
        rend.material.SetFloat("_FoamPercentage", foamAmount * Mathf.Clamp((1- tiltAngle / 90), 0, 1));
        rend.material.SetFloat("_FoamSmoothness", foamSmoothness * Mathf.Clamp((1- tiltAngle / 90), 0.5f, 1));

        if (!isStationary)
        {
            liqPos = (transform.up * fillAmount - transform.up * 0.5f) * transform.localScale.y;
            liq.position = rb.transform.position + new Vector3(0, fill * 0.2f - 0.1f, 0);
        }

    }
    
    void SetRenderWobble()
    {
        rend.material.SetFloat("_WobbleX", wobbleX);
        rend.material.SetFloat("_WobbleZ", wobbleZ);

        //Todo: wobble should follow a sine wave so that the wobble will slosh around the cup for a bit.
        wobbleX = Mathf.Lerp(wobbleX, 0, Time.fixedDeltaTime * thickness * 0.01f);
        wobbleZ = Mathf.Lerp(wobbleZ, 0, Time.fixedDeltaTime * thickness * 0.01f);

    }

    void SetWobble(float x, float z)
    {
        wobbleX = Mathf.Clamp(wobbleX + x * 0.5f, -maxWobble, maxWobble);
        wobbleZ = Mathf.Clamp(wobbleZ + z * 0.5f, -maxWobble, maxWobble);
    }

    void SetPour()
    {
        float tiltAngle = Vector3.Angle(transform.up, Vector3.up);
        if (tiltAngle < pourMinTilt && tiltAngle > -pourMinTilt)
        {
            SetPourPos(0, 0);
        }
        else
        {
            float angle = (Vector3.SignedAngle(Vector3.ProjectOnPlane(Vector3.up, transform.up), transform.right, transform.up) + 180f) * Mathf.Deg2Rad;
            Vector3 point = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
            point *= 0.5f * rimRadius;
            SetPourPos(point.x, point.z);
            SetPourRotation();
        }
    }

    void SetPourPos(float pourX, float pourZ)
    {
        pour.transform.localPosition = new Vector3(pourX, pour.transform.localPosition.y, pourZ);
    }

    void SetPourRotation()
    {
        var lookPos = pour.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        pour.rotation = rotation;
    }

    void CheckPour() {
        isPour = false;
        if(pour.position.y < liq.position.y)
        {
            isPour = true;
        }
    }

    void PourParticle()
    {
        if (container)
        {   
            if(isPour)
                container.Play();
            else
                container.Stop();
        }
    }

    private float EvaluateCurve(AnimationCurve curve, float position)
    {
        return curve.Evaluate(position);
    }

    public float AddLiquid(float addAmount)
    {
        float before = Mathf.Clamp(currentLiquidStored, 0, volume);
        currentLiquidStored = Mathf.Clamp(currentLiquidStored + addAmount, 0, volume);
        if (currentLiquidStored == volume)
            return 0;
        return currentLiquidStored - before;
    }
    public float SubstractLiquid(float substractAmount)
    {
        float before = currentLiquidStored > 0.000000001f ? currentLiquidStored : 0;
        currentLiquidStored = Mathf.Clamp(currentLiquidStored - substractAmount, 0, volume);
        if (currentLiquidStored == 0)
            return 0;
        return currentLiquidStored - before;
    }

    public float GetFillLevel()
    {
        return fillAmount;
    }

    public float GetLiquidStored()
    {
        return currentLiquidStored;
    }

    public void SetVisualFill(float amount)
    {
        fillAmount = Mathf.Clamp(amount, 0, 1);
    }
    public void EmptyContent()
    {
        if (container && !isStationary)
        {
            container.ClearFill();
        }
        else if (!overrideFill)
        {
            currentLiquidStored = 0;
        }
    }
}
