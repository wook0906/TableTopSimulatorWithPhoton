using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum ObjectState
    {
        Idle,
        Pickked,
        Lock
    }
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }
    public enum State
    {
        Idle,
        Moving,
    }

    public enum Layer
    {
        Ground = 6,
        Block = 7,
        Monster = 8,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Main,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum MouseEvent
    {
        LeftPress,
        LeftPointerDown,
        LeftPointerUp,
        LeftClick,
        RightPress,
        RightPointerDown,
        RightPointerUp,
        RightClick,
    }
    public enum CameraMode
    {
        QuaterView,
    }
}
