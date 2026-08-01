using TMPro;
using UnityEngine;

public class BuisnessAnimationManager : MonoBehaviour
{
    [SerializeField]
    private Animator plantAnimator;

    [SerializeField]
    private Animator wateringAnimator;

    [SerializeField]
    private Animator thumbAnimator;

    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private TextMeshProUGUI productionTime;

    private float wateringAnimatioLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimationClip wateringClip = wateringAnimator.runtimeAnimatorController.animationClips[0];
        wateringAnimatioLength = wateringClip.length;
    }

    // Update is called once per frame
    void Update()
    {
        float remainingTime = wateringAnimatioLength;
        if (float.TryParse(productionTime.text, out float value))
        {
            remainingTime = value;
            if (wateringAnimatioLength / 2 > remainingTime)
                wateringAnimator.SetBool("Producing", false);
        }
    }

    public void startAnimation(float deleyProduction)
    {
        AnimationClip plantClip = plantAnimator.runtimeAnimatorController.animationClips[0];
        float plantAnimationLength = plantClip.length;
        plantAnimator.speed = plantAnimationLength / deleyProduction;
 
        plantAnimator.SetBool("Producing", true);
        wateringAnimator.SetBool("Producing", true);
        particles.Play();
    }

    public void stopAnimation()
    {
        plantAnimator.SetBool("Producing", false);
        wateringAnimator.SetBool("Producing", false);
        particles.Stop();
        thumbAnimator.Play("ui_thumb");
    }
}
