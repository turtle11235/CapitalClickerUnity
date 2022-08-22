using System.Collections.Generic;

public abstract class Upgrade {
    public abstract  UpgradeID id {get;}

    public abstract string title { get; }
    public virtual string pricetag {get;}
    public abstract string description {get;}
    public virtual Dictionary<string, object> values {get;}

    public int UseCount = 0;
    public virtual int maxUses { get { return 1; } }
    public bool used;
    public bool visible = false;

    public Upgrade(bool visible=false, int useCount=0){
        this.visible = visible;
        this.UseCount = useCount;

        if (useCount >= this.maxUses) {
            this.used = true;
        }
    }

    public bool Trigger(){
        if (!this.used && !this.visible) {
            this.visible = this.CheckTriggerConditions();
        }
        return this.visible;
    }

    protected abstract bool CheckTriggerConditions();

    public abstract bool Cost();

    public void Use(){
        this.Execute();
        this.UseCount++;
        if (this.UseCount >= this.maxUses) {
            this.used = true;
        }
    }

    protected abstract void Execute();

    protected bool CheckDependencies(List<UpgradeID> ids){
        UpgradeManager um = UpgradeManager.Instance();
        return ids.TrueForAll(id => um.GetUpgrade(id).used);
    }

    protected bool CheckDependency(UpgradeID id)
    {
        return UpgradeManager.Instance().GetUpgrade(id).used;
    }

    public override string ToString() {
        if (!string.IsNullOrEmpty(this.pricetag)){
            return this.title + " " + this.pricetag;
        }
        else {
            return this.title;
        }
            
    }

    protected T getNext<T>(string name){
        T[] array = (T[])this.values[name];
        return (T)array[this.UseCount];
    }
}