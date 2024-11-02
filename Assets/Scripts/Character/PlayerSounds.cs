using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerSounds : MonoBehaviour
    {

        [SerializeField] private List<AudioClip> audioClipSteps;
        private AudioSource audioSourceSteps;

        private void Start()
        {
             audioSourceSteps = GetComponent<AudioSource>();
        }
        public void PlaySteps()
        {
            AudioClip audioClip = audioClipSteps[Random.Range(0, audioClipSteps.Count)];
            audioSourceSteps.clip = audioClip;
            audioSourceSteps.Play();
        }
    }
}
