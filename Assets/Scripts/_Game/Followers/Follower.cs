
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Followers
{
    public class Follower : MonoBehaviour
    {
        public Transform targetTf;
        [SerializeField] FollowType followType;
        [SerializeField] private UpdateMethod updateMethod;

        [SerializeField] private float smoothTime;
        private Vector3 smoothVel;
        [SerializeField] private float lerpMultiplier;
        private Vector3 offset;

        private Dictionary<FollowType, Action<Vector3>> followMethodDict;
        public enum UpdateMethod
        {
            Update,
            FixedUpdate,
            LateUpdate
        }
        public enum FollowType
        {
            Fixed,
            SmoothDamp,
            Lerp
        }
        private void Start()
        {
            offset = targetTf.position - transform.position;
            followMethodDict = new()
            {
                { FollowType.Fixed, FixedFollow },
                { FollowType.SmoothDamp, SmoothFollow },
                { FollowType.Lerp, Lerp },
            };
        }
        private void Update()
        {
            if (updateMethod != UpdateMethod.Update) return;
            Tick();
        }
        private void FixedUpdate()
        {
            if (updateMethod != UpdateMethod.FixedUpdate) return;
            Tick();
        }
        private void LateUpdate()
        {
            if (updateMethod != UpdateMethod.LateUpdate) return;
            Tick();
        }
        private void Tick()
        {
            var targetPos = targetTf.position - offset;
            followMethodDict[followType](targetPos);
        }
        private void FixedFollow(Vector3 targetPos)
        {
            transform.position = targetPos;
        }
        private void SmoothFollow(Vector3 targetPos)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref smoothVel, smoothTime);
        }
        private void Lerp(Vector3 targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpMultiplier);
        }
    }

}