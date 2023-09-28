// <copyright file="LongRunning.cs" company="Microsoft Corp.">
// Copyright (c) Microsoft Corp.. All rights reserved.
// </copyright>

namespace Microsoft.Movie.Store
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Long running service.
    /// </summary>
    public class LongRunning : BackgroundService
    {
        private readonly IActionItemWorkflow<SSv2SubscriptionInfo, CreateRuleActionItem> createRulesWorkflow;

        private readonly IActionItemWorkflow<SSv2SubscriptionInfo, CommitActionItem> commitWorkflow;

        /// <summary>
        /// Initializes a new instance of the <see cref="LongRunning"/> class.
        /// </summary>
        /// <param name="createRulesWorkflow">Workflow to populate s360 kpi.</param>
        /// <param name="commitWorkflow">Workflow to create commit action item in s360.</param>
        public LongRunning(
            IActionItemWorkflow<SSv2SubscriptionInfo, CreateRuleActionItem> createRulesWorkflow,
            IActionItemWorkflow<SSv2SubscriptionInfo, CommitActionItem> commitWorkflow)
        {
            this.createRulesWorkflow = createRulesWorkflow;
            this.commitWorkflow = commitWorkflow;
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.createRulesWorkflow.ProcessAsync().ConfigureAwait(false);
            await this.commitWorkflow.ProcessAsync().ConfigureAwait(false);
        }
    }
}
