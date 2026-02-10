using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class NPCSocializeState : HeirarchicalState
{
    [Export] public Area2D SocializeArea;

    public override void InitSubStates()
    {
        throw new NotImplementedException();
    }
}
