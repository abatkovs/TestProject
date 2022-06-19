using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//Prevent culling objects that are still visible.
public class WorldBendingFix : MonoBehaviour
{
    #region  Constants
    private const string BENDING_FEATURE = "ENABLE_BENDING";
    private const string PLANET_FEATURE = "ENABLE_BENDING_PLANET";
    private static readonly int BENDING_AMOUNT = Shader.PropertyToID("_BendAmount");
    #endregion
    
    [SerializeField] private bool enablePlanet = default;
    [Range(-0.1f, 0.1f)]
    [SerializeField] private float bendingAmount = 0.015f; //0.015 good value

    private float _prevAmount;

    private void Awake () 
    {
        if ( Application.isPlaying ) Shader.EnableKeyword(BENDING_FEATURE);
        else Shader.DisableKeyword(BENDING_FEATURE);

        if ( enablePlanet ) Shader.EnableKeyword(PLANET_FEATURE);
        else Shader.DisableKeyword(PLANET_FEATURE);

        UpdateBendingAmount();
    }

    private void OnEnable () 
    {
        if ( !Application.isPlaying ) return;
    
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void Update () 
    {
        if ( Math.Abs(_prevAmount - bendingAmount) > Mathf.Epsilon ) UpdateBendingAmount();
    }

    private void OnDisable () {
        ResetBending();
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void ResetBending()
    {
        bendingAmount = 0;
        UpdateBendingAmount();
    }

    private void UpdateBendingAmount (){
        _prevAmount = bendingAmount;
        Shader.SetGlobalFloat(BENDING_AMOUNT, bendingAmount);
    }

    /// <summary>
    /// Fix culling of objects on edges of screen
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="cam"></param>
    private static void OnBeginCameraRendering (ScriptableRenderContext ctx, Camera cam){
        cam.cullingMatrix = Matrix4x4.Ortho(-50, 50, -50, 50, 0.001f, 99) * cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering (ScriptableRenderContext ctx, Camera cam){
        cam.ResetCullingMatrix();
    }
}
