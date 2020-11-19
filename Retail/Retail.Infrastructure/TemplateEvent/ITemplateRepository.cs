using System;
using System.Threading.Tasks;

namespace Retail.Infrastructure.TemplateEvent
{
    public interface ITemplateRepository
    {
        Task Store(string text, DateTime addedOn);
        Task<Content> GetById(int id);
    }
}
