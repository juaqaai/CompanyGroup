using System.Collections.Generic;
using Orchard;
using Orchard.ContentManagement;

namespace Szmyd.Orchard.Modules.Menu.Services
{
    public interface IPartWatcher : IDependency
    {
        void Watch<T>(T part) where T : IContent;
        IEnumerable<T> Get<T>() where T : IContent;
    }
}
