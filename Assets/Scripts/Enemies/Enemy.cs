using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Enemy : Character
{
    
    protected Transform enemy;
    protected bool attack = false;
    protected bool playerHint = false;
    protected bool getHit = false;
    protected bool die = false;
    protected bool shoot = false;
    public float speed = 0f;
    protected Transform clonedTorpedo;
    public Transform enemyTorpedoPoint;
    //public Transform bubbleParticle;
    protected GameObject player;
    protected Vector2 playerInitialPosition, initialTorpedoPos;
    protected Animator enemyAnimator;
    public static bool torpedoHitted = false;
    public bool hasLight = false;
    public Light2D bodyLight, torchLight;
}
