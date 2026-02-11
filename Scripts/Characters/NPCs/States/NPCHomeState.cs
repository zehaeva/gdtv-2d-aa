using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class NPCHomeState : HeirarchicalState
{
    [Export] public Area2D HomeArea;

    public override void InitSubStates()
    {
        //throw new NotImplementedException();
        GD.Print("NPC Home State");
    }
}
