using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Models;

namespace Oathsworn.Business.Bosses
{
    public class ActionComponent
    {
        BossActionType Type { get; set; }
    }

    public class MoveActionComponent : ActionComponent
    {
        Direction Direction { get; set; }
        int Spaces { get; set; }
    }

    public class AttackActionComponent : ActionComponent
    {

    }

    public class Action
    {
        public List<ActionComponent> Components { get; set; }
    }

    public abstract class AbstractBoss
    {
        public AbstractBoss(Boss bossEntity)
        {
            BossEntity = bossEntity;
        }

        private Boss BossEntity { get; init; }
        public abstract List<BorderedPosition> GetSecondaryBossPositions();
        public abstract List<(BossPart, int)> GetBossParts();
        public abstract (BossPart, int) GetBossPartsFromPosition(IPosition position);
        public abstract List<Action> GetActions();
        public abstract bool IsBroken(BossPart bossPart);

    }
}
