namespace GitVersion
{
    using System.Collections.Generic;

    public class SimpleOrTutorialStep : ConfigInitWizardStep
    {
        protected override StepResult HandleResult(string result, Queue<ConfigInitWizardStep> steps, Config config)
        {
            if (result == "y")
            {
                steps.Enqueue(new PickBranchingStrategyStep());
            }
            else
            {
                steps.Enqueue(new EditConfigStep());
            }

            return StepResult.Ok();
        }

        protected override string GetPrompt(Config config)
        {
            return "Would you like to run an extended init? (more like a tutorial) (y/n)";
        }

        protected override string DefaultResult
        {
            get { return "n"; }
        }
    }
}