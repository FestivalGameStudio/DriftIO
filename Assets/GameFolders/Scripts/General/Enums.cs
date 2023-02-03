using UnityEngine;

namespace GameFolders.Scripts.General
{
    public enum GameState
    {
        Idle,
        Play,
        Finish
    }

    public enum MotionType
    {
        Move,
        LocalMove,
        Scale,
        Jump
    }

    public enum ShakeType
    {
        Position,
        Rotation,
        Scale
    }

    public enum UpdateType
    {
        FixedUpdate,
        Update,
        LateUpdate
    }
}