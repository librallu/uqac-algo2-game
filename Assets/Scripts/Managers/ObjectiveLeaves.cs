
using UnityEngine;

public abstract class ObjectiveEnd : ObjectiveTree
{
    protected ObjectiveManager objectiveManager;

    public ObjectiveEnd(ObjectiveManager manager)
    {
        objectiveManager = manager;
    }
}

public class ObjectiveZombunnies : ObjectiveEnd
{

    protected int nb;
    

    public ObjectiveZombunnies(ObjectiveManager manager, int n): base(manager)
    {
        nb = n;
    }

    public override ObjectiveTree simplify()
    {
        if ( objectiveManager.getZombunniesKilled() >= nb)
        {
            return new ObjectiveOk();
        }
        return this;
    }

    public override string toString()
    {
        return "zombunnies killed (" + objectiveManager.getZombunniesKilled() + "/" + nb + ")";
    }

}


public class ObjectiveZombears : ObjectiveEnd
{

    protected int nb;

    public ObjectiveZombears(ObjectiveManager manager, int n) : base(manager)
    {
        nb = n;
    }

    public override ObjectiveTree simplify()
    {
        if (objectiveManager.getZombearsKilled() >= nb)
        {
            return new ObjectiveOk();
        }
        return this;
    }

    public override string toString()
    {
        return "zombears killed (" + objectiveManager.getZombearsKilled() + "/" + nb + ")";
    }

}


public class ObjectiveHellephants : ObjectiveEnd
{

    protected int nb;

    public ObjectiveHellephants(ObjectiveManager manager, int n) : base(manager)
    {
        nb = n;
    }

    public override ObjectiveTree simplify()
    {
        if (objectiveManager.getHellephantsKilled() >= nb)
        {
            return new ObjectiveOk();
        }
        return this;
    }

    public override string toString()
    {
        return "hellephants killed (" + objectiveManager.getHellephantsKilled() + "/" + nb + ")";
    }

}


public class ObjectiveDuration : ObjectiveEnd
{

    protected int nb;

    public ObjectiveDuration(ObjectiveManager manager, int n) : base(manager)
    {
        nb = n;
    }

    public override ObjectiveTree simplify()
    {
        if (objectiveManager.getDuration() >= nb)
        {
            return new ObjectiveOk();
        }
        return this;
    }

    public override string toString()
    {
        return "duration (" + objectiveManager.getDuration() + "/" + nb + ")";
    }

}