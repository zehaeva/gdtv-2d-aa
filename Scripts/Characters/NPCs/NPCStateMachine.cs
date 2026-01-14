using Godot;
using Godot.Collections;

public partial class NPCStateMachine : StateMachine
{
    [Export] public Dictionary<int, NPCState> Schedule { get; set; }


    public override void SwitchState<T>()
    {

    }
}
