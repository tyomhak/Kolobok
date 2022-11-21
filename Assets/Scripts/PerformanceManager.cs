using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    [Tooltip("Set to 0 to ignore.")]
    [SerializeField] int _fps_target;


    private void Awake()
    {
        if (_fps_target != 0)
            Invoke("_set_fps_target", 2f);
    }


    void SetFpsTarget(int targetFramerate)
    {
        _fps_target = targetFramerate;
        _set_fps_target();
    }

    void _set_fps_target()
    {
        Application.targetFrameRate = _fps_target;
    }
}
