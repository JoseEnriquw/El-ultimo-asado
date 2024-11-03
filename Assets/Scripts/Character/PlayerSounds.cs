using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerSounds : MonoBehaviour
    {
        [Header("Footstep Sounds")]
        [SerializeField] private List<AudioClip> audioClipGrassSteps;
        [SerializeField] private List<AudioClip> audioClipHouseSteps;
        [SerializeField] private Transform raycastOrigin;
        private readonly float rayDistance = 1.5f;

        private AudioSource audioSourceSteps;

        private void Start()
        {
            audioSourceSteps = GetComponent<AudioSource>();
        }

        public void PlaySteps()
        {
            Surface.ESurfaceType surfaceType = GetSurfaceType();

            List<AudioClip> audioClipSteps = GetAudioClipsForSurface(surfaceType);

            if (audioClipSteps.Count > 0)
            {
                AudioClip audioClip = audioClipSteps[Random.Range(0, audioClipSteps.Count)];
                audioSourceSteps.PlayOneShot(audioClip);
            }
        }

        private Surface.ESurfaceType GetSurfaceType()
        {             
            if (Physics.Raycast(raycastOrigin.position, Vector3.down, out RaycastHit hit, rayDistance))
            {
                if (hit.collider.TryGetComponent<Surface>(out var surface))
                {
                    return surface.surfaceType;
                }
            }

            return Surface.ESurfaceType.Grass;
        }

        private List<AudioClip> GetAudioClipsForSurface(Surface.ESurfaceType surfaceType)
        {
            return surfaceType switch
            {
                Surface.ESurfaceType.Grass => audioClipGrassSteps,
                Surface.ESurfaceType.HouseFloor => audioClipHouseSteps,
                _ => audioClipGrassSteps,
            };
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position+Vector3.down*rayDistance);
        }
    }
}
