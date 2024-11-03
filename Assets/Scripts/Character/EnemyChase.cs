using UnityEngine;

namespace Assets.Scripts.Character
{
    enum EnemyStatesEnum { Pursue,WaitOutHouse,Stay }
    public class EnemyChase : MonoBehaviour
    {
        private Transform destiny;
        [SerializeField] private Transform player;
        [SerializeField] private Transform waitOutHouse;
        [SerializeField] private float speed;
        [SerializeField] private float stopDistance;
        [SerializeField] private AudioClip chaseSound;
        [SerializeField] private EnemyStatesEnum currentState;
        private Animator animator;

        private AudioSource audioSource;
        //private bool hasSpoken = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            // Reproducir sonido de persecución si está asignado
            if (chaseSound != null)
            {
                audioSource.clip = chaseSound;
                audioSource.Play();
            }
        }

        private void Update()
        {
            switch(currentState)
            {
                case EnemyStatesEnum.Pursue:
                        destiny = player;
                        MoveToDestiny(stopDistance);
                    break;
                case EnemyStatesEnum.WaitOutHouse:
                        destiny= waitOutHouse;
                        MoveToDestiny(1f);
                    if (!animator.GetBool("isRunning"))
                    {
                        gameObject.transform.Rotate(new(0, -120, 0));
                        currentState = EnemyStatesEnum.Stay;
                    }
                    break;
                case EnemyStatesEnum.Stay:
                        animator.SetBool("isRunning", false);
                    break;
                default:
                        animator.SetBool("isRunning", false);
                    break;
            }
        }

        private void MoveToDestiny(float stopDistance)
        {
            float distance = Vector3.Distance(transform.position, destiny.position);
            if (distance > stopDistance)
            {
                Vector3 directionToPlayer = (destiny.position - transform.position).normalized;
                Vector3 moveDirection = directionToPlayer;

                // Mover al enemigo
                transform.position += moveDirection.normalized * speed * Time.deltaTime;

                animator.SetBool("isRunning", true);
                Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }
}
