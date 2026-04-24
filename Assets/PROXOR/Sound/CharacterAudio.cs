using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;

namespace KinematicCharacterController.Examples
{
    public class CharacterAudio : MonoBehaviour
    {
        public ExampleCharacterController _controller;
        private AudioSource _audioSource;

        [SerializeField] float _walkStepInterval = 0.5f;
        [SerializeField] float _sprintStepInterval = 0.3f;
        [SerializeField] float _velocityThreshold = 0.1f;
        [SerializeField] float _landVelocityThreshold = 2f;

        [SerializeField] List<AudioClip> _footstepSounds;
        [SerializeField] List<AudioClip> _sprintSounds;
        [SerializeField] List<AudioClip> _jumpSounds;
        [SerializeField] List<AudioClip> _landSounds;

        private float _stepTimer;
        private float _maxFallVelocity;

        private int _walkIndex = 0;
        private int _sprintIndex = 0;
        private int _jumpIndex = 0;
        private int _landIndex = 0;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            var motor = _controller.Motor;

            this.UpdateAsObservable()
                .Select(_ => GetMovementState())
                .Subscribe(state =>
                {
                    if (!state.IsGrounded)
                    {
                        if (motor.Velocity.y < _maxFallVelocity)
                        {
                            _maxFallVelocity = motor.Velocity.y;
                        }
                    }

                    if (state.IsGrounded && state.IsMoving)
                    {
                        HandleFootstepTimer(state.IsSprinting);
                    }
                    else
                    {
                        if (_audioSource.isPlaying && state.IsGrounded)
                        {
                            _audioSource.Stop();
                        }
                        _stepTimer = _walkStepInterval;
                    }
                })
                .AddTo(this);
            motor.ObserveEveryValueChanged(m => m.GroundingStatus.IsStableOnGround)
                .Pairwise()
                .Subscribe(change =>
                {
                    if (change.Previous && !change.Current)
                    {
                        if (motor.Velocity.y > 0.1f)
                        {
                            PlayEffectSequential(_jumpSounds, ref _jumpIndex, 1.0f, false);
                        }
                        _maxFallVelocity = 0f;
                    }
                    else if (!change.Previous && change.Current)
                    {
                        if (_maxFallVelocity < -_landVelocityThreshold)
                        {
                            PlayEffectSequential(_landSounds, ref _landIndex, 0.8f, false);
                        }
                        _maxFallVelocity = 0f;
                        _stepTimer = 0.1f;
                    }
                })
                .AddTo(this);
        }
        private struct MovementState
        {
            public bool IsGrounded;
            public bool IsMoving;
            public bool IsSprinting;
        }
        private MovementState GetMovementState()
        {
            Vector3 horizontalVelocity = new Vector3(_controller.Motor.Velocity.x, 0f, _controller.Motor.Velocity.z);
            return new MovementState
            {
                IsGrounded = _controller.Motor.GroundingStatus.IsStableOnGround,
                IsMoving = horizontalVelocity.magnitude > _velocityThreshold,
                IsSprinting = _controller.IsSprintingActual
            };
        }
        private void HandleFootstepTimer(bool isSprinting)
        {
            float interval = isSprinting ? _sprintStepInterval : _walkStepInterval;
            _stepTimer += Time.deltaTime;

            if (_stepTimer >= interval)
            {
                if (isSprinting && _sprintSounds.Count > 0)
                {
                    PlayEffectSequential(_sprintSounds, ref _sprintIndex, 1f, true);
                }
                else if (_footstepSounds.Count > 0)
                {
                    PlayEffectSequential(_footstepSounds, ref _walkIndex, 0.7f, true);
                }

                _stepTimer = 0f;
            }
        }
        private void PlayEffectSequential(List<AudioClip> clips, ref int index, float volume, bool interrupt)
        {
            if (clips == null || clips.Count == 0) return;

            _audioSource.pitch = Random.Range(0.85f, 1.05f);

            if (interrupt)
            {
                _audioSource.clip = clips[index];
                _audioSource.volume = volume;
                _audioSource.Play();
            }
            else
            {
                _audioSource.PlayOneShot(clips[index], volume);
            }
            index = (index + 1) % clips.Count;
        }
    }
}