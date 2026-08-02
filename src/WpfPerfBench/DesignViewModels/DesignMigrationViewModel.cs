using WpfPerfBench.Core.Enums;
using WpfPerfBench.ViewModels;
using WpfPerfBench.Wrappers;

namespace WpfPerfBench.DesignViewModels;

public class DesignMigrationViewModel : MigrationViewModel
{
    public DesignMigrationViewModel() 
        : base(null, null, null, null, null)
    {
        var itemPending = new MigrationItem("20260709162035_InitialPostgres")
        {
            Status = MigrationStatus.Pending
        };
        Items.Add(itemPending);

        var itemApplied = new MigrationItem("20260709163316_SeedCategory")
        {
            Status = MigrationStatus.Applied
        };
        Items.Add(itemApplied);

        var itemFailed = new MigrationItem("20260709165608_AddItemEntity")
        {
            Status = MigrationStatus.Failed
        };
        Items.Add(itemFailed);

        var itemSkipped = new MigrationItem("20260709170136_AddItemCategoryReference")
        {
            Status = MigrationStatus.Skipped
        };
        Items.Add(itemSkipped);

        var itemProcessing = new MigrationItem("20260709214829_UpdateContextByDbSet")
        {
            Status = MigrationStatus.Processing
        };
        Items.Add(itemProcessing);

        Items.Add(new MigrationItem("20260710112345_ChangeSeeding"));
        Items.Add(new MigrationItem("20260716120602_HasData"));
    }
}