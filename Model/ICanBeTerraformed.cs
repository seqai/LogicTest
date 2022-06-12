namespace LogicTest.Model
{
    internal interface ICanBeTerraformed : ICanSustainLife
    {
        bool Terraform(ITerraformer terraformer);
    }

    internal interface ITerraformer {
        int TerraformingSuccessRate { get; }
    }
}
