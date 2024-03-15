using ctutorial.Data;
using Microsoft.EntityFrameworkCore;

namespace ctutorial.Data
{
    public class ReplicaContext : CtutorialContext<ReplicaContext>
    {
        public ReplicaContext(DbContextOptions<ReplicaContext> options) : base(options) { }
    }
}
