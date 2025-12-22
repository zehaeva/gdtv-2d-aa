using Godot;
using System;

public partial class SlimeDeathState : SlimeState, IDeathState
{
    protected async override void EnterState()
    {
        ((Slime)characterNode).GPUParticles2DNode.Emitting = true;
        ((Slime)characterNode).AnimatedSprite2DNode.Visible = false;
        characterNode.AnimationPlayerNode.Stop();
        characterNode.CollisionShape2DNode.SetDeferred("disabled", true);

        await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);

        if (characterNode.PathNode != null)
        {
            characterNode.PathNode.QueueFree();
        }
    }
}
