namespace ctutorial.Data
{
    public class MainContext
    {
        public MainContext(MasterContext masterContext, ReplicaContext replicaContext)
        {
            GetMasterContext = masterContext;
            GetReplicaContext = replicaContext;
        }

        public MasterContext GetMasterContext { get; }

        public ReplicaContext GetReplicaContext { get; }
    }
}
