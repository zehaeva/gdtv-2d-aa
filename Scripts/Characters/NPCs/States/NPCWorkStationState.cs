using Godot;
using System;

public partial class NPCWorkStationState : NPCState
{
    public new StateType StateType = StateType.WORK;
    // need workstation area
    [Export] public Area2D WorkArea;
    // list of materials needed for work
    [Export] public InventoryItem[] RequiredMaterials;

    protected override void EnterState()
    {
        characterNode.AnimationPlayerNode.Play(GameConstants.ANIM_WORKING);

        // we need a set of hooks/signals to handle interupting states
        // attacked?

        // talked to?

        // suddenly died?

        // Check for requirements
        foreach (var item in RequiredMaterials)
        {
            if (!characterNode.Inventory.HasItem(item, item.Amount))
            {
                GD.Print("Missing required material: " + item.ItemName);
                // maybe switch to a different state or just stop working
                return;
            }
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                              
}
