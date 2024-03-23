using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(PlayerAnimationController))]
public class AnimationSetupEditor : Editor
{
    private string animationName = ""; // Default animation name
    List<AnimationClip> createdAnimations = new List<AnimationClip>();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerAnimationController animationController = (PlayerAnimationController)target;

        // Animation name field
        animationName = EditorGUILayout.TextField("Animation Name", animationName);

        if (GUILayout.Button("Setup Animations"))
        {
            createdAnimations = new List<AnimationClip>();

            // Create animation clips
            CreateAnimation("IdleUp", animationController.idleUp);
            CreateAnimation("IdleDown", animationController.idleDown);
            CreateAnimation("IdleSide", animationController.idleSide);
            CreateAnimation("WalkUp", animationController.walkup);
            CreateAnimation("WalkDown", animationController.walkDown);
            CreateAnimation("WalkSide", animationController.walkSide);

            // Create animator override controller
            CreateAnimatorOverrideController(animationController);
        }
    }


    private void CreateAnimation(string animationKey, Sprite[] sprites)
    {
        string animationClipName = animationName + animationKey;
        AnimationClip clip = CreateAnimationClip(animationClipName, sprites);
        createdAnimations.Add(clip);
    }

    private Sprite[] LoadSpritesFromFolder(string folderPath)
    {
        string[] spritePaths = AssetDatabase.FindAssets("t:2D/Sprites/Character", new[] { folderPath });

        List<Sprite> sprites = new List<Sprite>();
        foreach (string spritePath in spritePaths)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(spritePath);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite != null)
            {
                sprites.Add(sprite);
            }
        }

        return sprites.ToArray();
    }

    private AnimationClip CreateAnimationClip(string name, Sprite[] sprites)
    {
        AnimationClip clip = new AnimationClip();
        clip.frameRate = 10; 

        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = i / clip.frameRate;
            keyFrames[i].value = sprites[i];
        }

        AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, keyFrames);

        // Set loop to true
        AnimationClipSettings clipSettings = AnimationUtility.GetAnimationClipSettings(clip);
        clipSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(clip, clipSettings);

        // Create parent directories if they don't exist
        string directoryPath = "Assets/2D/Animations/Body Items/" + animationName;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Create animation asset
        AssetDatabase.CreateAsset(clip, "Assets/2D/Animations/Body Items/" + animationName + "/" + name + ".anim");
        AssetDatabase.SaveAssets();

        return clip;
    }



    private void CreateAnimatorOverrideController(PlayerAnimationController animationController)
    {
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = animationController.baseAnimator.runtimeAnimatorController;

        // Create parent directories if they don't exist
        string directoryPath = "Assets/2D/Animations/Body Items/" + animationName;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Set new animations
        overrideController["PlayerIdleUp"] = createdAnimations[0];
        overrideController["PlayerIdleDown"] = createdAnimations[1];
        overrideController["PlayerIdleSide"] = createdAnimations[2];
        overrideController["PlayerWalkUp"] = createdAnimations[3];
        overrideController["PlayerWalkDown"] = createdAnimations[4];
        overrideController["PlayerWalkSide"] = createdAnimations[5];

        // Create override controller asset
        AssetDatabase.CreateAsset(overrideController, "Assets/2D/Animations/Body Items/" + animationName + "/" + animationName + "OverrideController.overrideController");
        AssetDatabase.SaveAssets();
    }

}
