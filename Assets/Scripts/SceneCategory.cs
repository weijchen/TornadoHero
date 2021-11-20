using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCategory
{
    private SceneCategory(string value)
    {
        Value = value;
    }
    
    public string Value { get; private set; }
    
    public static SceneCategory Start
    {
        get { return new SceneCategory("StartScene"); }
    }
    
    public static SceneCategory Main
    {
        get { return new SceneCategory("MainScene"); }
    }
    
    public static SceneCategory End
    {
        get { return new SceneCategory("EndScene"); }
    }
}
