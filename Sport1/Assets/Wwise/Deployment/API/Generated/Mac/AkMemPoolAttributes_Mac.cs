#if UNITY_STANDALONE_OSX || (UNITY_EDITOR_OSX && UNITY_STANDALONE_OSX) || (UNITY_EDITOR_OSX && UNITY_IOS) || (UNITY_EDITOR_OSX && UNITY_ANDROID)
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


public enum AkMemPoolAttributes {
  AkNoAlloc = 0,
  AkMalloc = 1,
  AkAllocMask = AkNoAlloc|AkMalloc,
  AkFixedSizeBlocksMode = 1 << 3,
  AkBlockMgmtMask = AkFixedSizeBlocksMode
}
#endif // #if UNITY_STANDALONE_OSX || (UNITY_EDITOR_OSX && UNITY_STANDALONE_OSX) || (UNITY_EDITOR_OSX && UNITY_IOS) || (UNITY_EDITOR_OSX && UNITY_ANDROID)
