

public abstract class Upgrade {
    public abstract UpgradeID id {get;}

    public abstract string title { get; }
    public virtual string pricetag {get;}
    public abstract string description {get;}

    public int useCount;
    public int maxUses;
    public bool used;
    public bool visible;

    public Upgrade(bool visible=false, bool used=false, int useCount=0, int maxUses=0){
        this.visible = visible;
        this.used = used;
        this.useCount = useCount;
        this.maxUses = maxUses;
    }

    public bool Trigger(){
        if (!this.used && !this.visible) {
            this.visible = this.CheckTriggerConditions();
        }
        return this.visible;
    }

    protected abstract bool CheckTriggerConditions();

    public void Use(){
        this.Execute();
        this.useCount++;
        if (this.useCount >= this.maxUses) {
            this.used = true;
        }
    }

    protected abstract void Execute();

    private bool CheckDependencies(){
        return true;
    }

    public override string ToString() {
        return this.title;
    }
}