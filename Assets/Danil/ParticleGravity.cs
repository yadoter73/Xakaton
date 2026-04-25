using UnityEngine;

public class ParticleGravity : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesSystem;
    [SerializeField] private float attractionForce = 10f; 

    private ParticleSystem.Particle[] particles;

    void LateUpdate()
    {
        if (particlesSystem == null) return;

      
        if (particles == null || particles.Length < particlesSystem.main.maxParticles)
            particles = new ParticleSystem.Particle[particlesSystem.main.maxParticles];

       
        int particleCount = particlesSystem.GetParticles(particles);

        
        for (int i = 0; i < particleCount; i++)
        {
            
            Vector3 direction = transform.position - particles[i].position;

            
            if (direction.magnitude > 0.1f)
            {
                particles[i].velocity += direction.normalized * attractionForce * Time.deltaTime;
            }
        }

       
        particlesSystem.SetParticles(particles, particleCount);
    }
}
