using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioClip track;      
    public Transform player;     
    public float maxDist = 20f;  

    AudioSource source;
    AudioLowPassFilter filter;

    void Start()
    {
        
        source = gameObject.AddComponent<AudioSource>();
        filter = gameObject.AddComponent<AudioLowPassFilter>();

        source.clip = track;
        source.loop = true;
        source.playOnAwake = true;
        source.Play();
    }

    void Update()
    {
        float d = Vector3.Distance(transform.position, player.position);

        float percent = Mathf.Clamp01(1f - (d / maxDist));

        source.volume = percent;
        filter.cutoffFrequency = Mathf.Lerp(400, 2000, percent);
    }
}