using ComputerUse.Agent.Core.Tools;

namespace ComputerUse.Agent.Infrastructure.Anthropic
{
    internal record CommandContext
    {
        public required string Identifier { get; init; }

        public required string ToolId { get; init; }

        public required string? Text { get; init; }
        
        public int? Y { get; init; }

        public int? X { get; init; }

        public MouseClickOptionsContract? ClickOptions { get; init; }

        public MouseScrollDirectionContract? ScrollDirection { get; init; }

        public int? ScrollAmount { get; init; }

        // ????
        public int? Duration { get; init; }

        public override string ToString()
        {
            return $"Identifier={Identifier ?? "<null>"}"
                + $", ToolId={ToolId ?? "<null>"}"
                + $", Text={Text ?? "<null>"}"
                + $", Y={Y?.ToString() ?? "<null>"}"
                + $", X={X?.ToString() ?? "<null>"}"
                + $", ClickOptions={ClickOptions?.ToString() ?? "<null>"}"
                + $", ScrollDirection={ScrollDirection?.ToString() ?? "<null>"}"
                + $", ScrollAmount={ScrollAmount?.ToString() ?? "<null>"}"
                + $", Duration={Duration?.ToString() ?? "<null>"}";
        }
    }
}
