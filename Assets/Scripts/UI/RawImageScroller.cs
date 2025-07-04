using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageScroller : MonoBehaviour
{
   [SerializeField] private Vector2 scrollSpeed = new Vector2(0.5f, 0.5f);
   [SerializeField] private RawImage rawImage;

   private Rect _uvRect;

   private void Start()
   {
      _uvRect = rawImage.uvRect;
   }

   private void Update()
   {
      _uvRect.x +=scrollSpeed.x *Time.deltaTime;
      _uvRect.y += scrollSpeed.y * Time.deltaTime;
      rawImage.uvRect = _uvRect;
   }
}
