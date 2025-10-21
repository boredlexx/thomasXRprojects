using UnityEngine;

public enum FeatureUsage
{
    Once, //use once
    Toggle //if you want to use it more than once
}


public class CoreFeatures : MonoBehaviour
{
    /* Property - commmon way to access code that exisists ouside of this class
     * Can create public variables and access them that way, or you can use PRoperties
     * Properties ENCAPSULATES variables as fields
     * GET accesor (READ) - returns the encapsulated variable values
     * SET accesor (WRITE) - alocates new values to the property field
     * PROPERTY values use PascalCase
     */

    public bool AudioSFXSourceCreated { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipOnStart { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipOnStop { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    {
        MakeSFXAudioSource();
    }

    public void MakeSFXAudioSource()
    {
        audioSource = GetComponent<AudioSource>();

        //if this is equal to null, create it here

        if(audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();  
        }

        //whether it is null ot not, we still need to make sur ethis is true
        //on awakw create this audiosource

        AudioSFXSourceCreated = true;
    }
}
