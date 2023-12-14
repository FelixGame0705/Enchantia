using BehaviorTree;
using System.Collections.Generic;

public class BossBT : Tree
{
    public BossController BossController;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node> {
            new Sequence(new List<Node>
            {
                new CheckTargetInRange(transform, BossController.target, BossController.BossDataConfig.EnemyStats.RangeAttack),
                new TaskAttack_1(BossController.transform, BossController.target)
            }),
            new TaskGoToTarget(BossController.transform, BossController.target, BossController.BossDataConfig)
        }
            );
        //Node root = new TaskGoToTarget(transform, BossController.target, BossController.BossDataConfig);
        return root;    
    }

}
