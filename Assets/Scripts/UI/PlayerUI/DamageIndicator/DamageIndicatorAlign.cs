using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorAlign : MonoBehaviour
{
    [SerializeField] private Image radialImage;
    [SerializeField] private RectTransform radialTrans;
    [SerializeField] private CanvasGroup canvasGroup;

    private float rotOffset;
    private Vector2 target;

    [SerializeField] private float decayRate;
    [SerializeField] private float startDecay;
    public float overrideTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Decay();
        FaceTarget();
    }


    public void Initialise(float input, Vector3 inputTarget)
    {
        float maxDeg = 0.25f;
        float maxDam = 100f;
        float clampedInput = Mathf.Clamp(input, 0, maxDam);
        float asPercent = clampedInput / maxDam;
        float asFill = asPercent * maxDeg;
        radialImage.fillAmount = asFill;

        rotOffset = 360 * (asFill / 2);
        target = new Vector2(inputTarget.x, inputTarget.z);
        startDecay = input;
        
    }
    private void Decay()
    {
        if (startDecay <= 0.05)
        {
            startDecay = 0;
            canvasGroup.alpha = 0;
            return;
        }
        startDecay = Mathf.Lerp(startDecay, 0, decayRate * Time.deltaTime);
        canvasGroup.alpha = startDecay;
    }

    private void FaceTarget()
    {
        float cameraYrot = Camera.main.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
        Vector2 cameraFacing = new Vector2(Mathf.Sin(cameraYrot), Mathf.Cos(cameraYrot));
        Vector2 towardTarget = (target - cameraPos).normalized;

        float rot = Vector2.SignedAngle(cameraFacing,towardTarget);
        radialTrans.rotation = Quaternion.Euler(new Vector3(0, 0, rot + rotOffset));
    }
}
