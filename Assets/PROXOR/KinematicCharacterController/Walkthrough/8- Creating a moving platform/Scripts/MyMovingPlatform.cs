using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

namespace KinematicCharacterController.Walkthrough.MovingPlatform
{
    public struct MyMovingPlatformState
    {
        public PhysicsMoverState MoverState;
        public float DirectorTime;
    }

    public class MyMovingPlatform : MonoBehaviour, IMoverController
    {
        public PhysicsMover Mover;
        public PlayableDirector Director;
        private Transform _transform;

        private void Start()
        {
            _transform = this.transform;
            Mover.MoverController = this;
        }
        public void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
        {
            Vector3 _positionBeforeAnim = _transform.position;
            Quaternion _rotationBeforeAnim = _transform.rotation;

            EvaluateAtTime(Time.time);

            goalPosition = _transform.position;
            goalRotation = _transform.rotation;

            _transform.position = _positionBeforeAnim;
            _transform.rotation = _rotationBeforeAnim;
        }

        public void EvaluateAtTime(double time)
        {
            Director.time = time % Director.duration;
            Director.Evaluate();
        }
    }
}