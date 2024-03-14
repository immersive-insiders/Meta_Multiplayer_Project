using Oculus.Movement;
using Oculus.Movement.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.GraphicsBuffer;

namespace Oculus.Movement.Effects
{
    [DefaultExecutionOrder(300)]
    public class LinkPlayerAvater : MonoBehaviour
    {
        [System.Serializable]
        public struct MirroredTransformPair
        {
            /// <summary>
            /// The name of the mirrored transform pair.
            /// </summary>
            [HideInInspector] public string Name;

            /// <summary>
            /// The original transform.
            /// </summary>
            [Tooltip(LateMirroredObjectTooltips.MirroredTransformPairTooltips.OriginalTransform)]
            public Transform OriginalTransform;

            /// <summary>
            /// The mirrored transform.
            /// </summary>
            [Tooltip(LateMirroredObjectTooltips.MirroredTransformPairTooltips.MirroredTransform)]
            public Transform MirroredTransform;
        }

        /// <summary>
        /// Returns the original transform.
        /// </summary>
        public Transform OriginalTransform => _transformToCopy;

        /// <summary>
        /// Returns the mirrored transform.
        /// </summary>
        public Transform MirroredTransform => _myTransform;

        /// <summary>
        /// The transform which transform values are being mirrored from.
        /// </summary>

        private Transform _transformToCopy;

        /// <summary>
        /// The target transform which transform values are being mirrored to.
        /// </summary>
        [SerializeField] private Transform _myTransform;

        /// <summary>
        /// The array of mirrored transform pairs.
        /// </summary>
        
        public MirroredTransformPair[] _mirroredTransformPairs;

        /// <summary>
        /// Mirror scale.
        /// </summary>
        [SerializeField]
        [Tooltip(LateMirroredObjectTooltips.MirrorScale)]
        protected bool _mirrorScale = false;

        private void Awake()
        {
            //Assert.IsNotNull(_transformToCopy);
            //Assert.IsNotNull(_myTransform);

            //foreach (var mirroredTransformPair in _mirroredTransformPairs)
            //{
            //    Assert.IsNotNull(mirroredTransformPair.OriginalTransform);
            //    Assert.IsNotNull(mirroredTransformPair.MirroredTransform);
            //}
        }

        bool BoneMaped = false;
        /// <summary>
        /// Mirror in late update.
        /// </summary>
        private void LateUpdate()
        {
            if (!BoneMaped)
                return;
            _myTransform.localPosition = _transformToCopy.localPosition;
            _myTransform.localRotation = _transformToCopy.localRotation;
            if (_mirrorScale)
            {
                _myTransform.localScale = _transformToCopy.localScale;
            }
            foreach (var transformPair in _mirroredTransformPairs)
            {
                transformPair.MirroredTransform.localPosition = transformPair.OriginalTransform.localPosition;
                transformPair.MirroredTransform.localRotation = transformPair.OriginalTransform.localRotation;
                if (_mirrorScale)
                {
                    transformPair.MirroredTransform.localScale = transformPair.OriginalTransform.localScale;
                }
            }
        }

        public void LinkPlayerAvatarToVR()
        {
            Transform characterTransform = FindObjectOfType<ActualPlayer>().transform;

            Debug.Log(" FOUNBD THE ACTUAL CHARCATER = " + characterTransform.name + " PARENT " + characterTransform.parent.name);
            _transformToCopy = characterTransform;
            MapBones();

        }

        public void MapBones()
        {
            
            var childTransformsOriginal = new List<Transform>(this.OriginalTransform.GetComponentsInChildren<Transform>(true));
            var childTransformsMirror = new List<Transform>(this.MirroredTransform.GetComponentsInChildren<Transform>(true));

            Debug.Log(childTransformsOriginal);
            Debug.Log(childTransformsMirror);

            _mirroredTransformPairs = new MirroredTransformPair[childTransformsMirror.Count-1];
            for (int i = 0; i < childTransformsMirror.Count -1; i++)
            {

                _mirroredTransformPairs[i].Name = childTransformsMirror[i+1].name;
                _mirroredTransformPairs[i].OriginalTransform = childTransformsOriginal[i+1];
                _mirroredTransformPairs[i].MirroredTransform = childTransformsMirror[i+1];
                
            }
            BoneMaped = true;

        }
    }
}

