namespace ChatBotHub.Domain.Common.Models;

public class Audit {
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public void PerformCreated() {
        CreatedOn = DateTime.UtcNow;
        ModifiedOn = DateTime.UtcNow;
    }

    public void PerformModified() {
        ModifiedOn = DateTime.UtcNow;
    }
}
