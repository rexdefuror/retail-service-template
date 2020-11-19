using System;

namespace Retail.Contracts
{
    public class TemplateEventOccurred
    {
        public string Content { get; set; }
        public DateTimeOffset Created => DateTimeOffset.UtcNow;
    }
}
