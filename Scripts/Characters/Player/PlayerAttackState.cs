using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
    }

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_ATTACK + DirectionFacing);
        characterNode.AnimationPlayerNode.AnimationFinished += HandleAnimationFinished;

        characterNode.HitBoxNode.BodyEntered += HandleBodyEntered;
    }
    protected override void ExitState()
    {
        characterNode.AnimationPlayerNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.HitBoxNode.BodyEntered -= HandleBodyEntered;
    }

    private void HandleBodyEntered(Node2D body)
    {
        if (comboCounter != maxComboCount) { return; }
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);

        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        characterNode.ToggleHitBox(true);
    }

    private void PerformHit()
    {
        ////Vector2 newPosition = characterNode.Sprite2DNode.FlipH ? Vector2.Left : Vector2.Right;
        ////float distanceMulti = 0.75f;
        ////newPosition *= distanceMulti;

        ////characterNode.HitBoxNode.Position = newPosition;
        ////characterNode.ToggleHitBox(false);


        //if (!AttackDurationTimer.IsStopped()) { return; }

        ////is_attacking = true;
        //Sword.Visible = true;
        ////Sword / SwordArea2D.monitoring = true;
        ////Sword / SwordSFX.Play();
        //AttackDurationTimer.Start();

        //Velocity = new Vector2(0, 0);

        //switch (AnimatedSprite2DNode.Animation)
        //{
        //    case GameConstants.ANIM_MOVE_UP:
        //        AnimatedSprite2DNode.Play(GameConstants.ANIM_ATTACK_UP);
        //        AnimationPlayerNode.Play("attack_sword_up");
        //        break;
        //    case GameConstants.ANIM_MOVE_RIGHT:
        //        AnimatedSprite2DNode.Play(GameConstants.ANIM_ATTACK_RIGHT);
        //        AnimationPlayerNode.Play("attack_sword_right");
        //        break;
        //    case GameConstants.ANIM_MOVE_DOWN:
        //        AnimatedSprite2DNode.Play(GameConstants.ANIM_ATTACK_DOWN);
        //        AnimationPlayerNode.Play("attack_sword_down");
        //        break;
        //    case GameConstants.ANIM_MOVE_LEFT:
        //        AnimatedSprite2DNode.Play(GameConstants.ANIM_ATTACK_LEFT);
        //        AnimationPlayerNode.Play("attack_sword_left");
        //        break;
        //}
    }
}
